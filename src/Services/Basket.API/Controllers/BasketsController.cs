using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories.Interfaces;
using Confluent.Kafka;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared.DTOs.Basket;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    //private readonly IPublishEndpoint _publishEndpoint;
    private readonly IProducer<Null, string> _kafkaProducer;
    private readonly StockItemGrpcService _stockItemGrpcService;

    public BasketsController(IBasketRepository basketRepository, IMapper mapper,
        //IPublishEndpoint publishEndpoint,
        IProducer<Null, string> kafkaProducer,
        StockItemGrpcService stockItemGrpcService)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        //_publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        _kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
        _stockItemGrpcService = stockItemGrpcService ?? throw new ArgumentNullException(nameof(stockItemGrpcService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{username}", Name = "GetBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Cart>> GetBasket([Required] string username)
    {
        var cart = await _basketRepository.GetBasketByUserName(username);
        var result = _mapper.Map<CartDto>(cart) ?? new CartDto(username);

        return Ok(result);
    }

    [HttpPost(Name = "UpdateBasket")]
    [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CartDto>> UpdateBasket([FromBody] CartDto model)
    {
        // Communicate with Inventory.Product.Grpc and check quantity available of products
        foreach (var item in model.Items)
        {
            var stock = await _stockItemGrpcService.GetStock(item.ItemNo);
            item.SetAvailableQuantity(stock.Quantity);
        }

        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.UtcNow.AddHours(10));
        //     .SetSlidingExpiration(TimeSpan.FromMinutes(10));

        var cart = _mapper.Map<Cart>(model);
        var updatedCart = await _basketRepository.UpdateBasket(cart, options);
        var result = _mapper.Map<CartDto>(updatedCart);
        return Ok(result);
    }

    [HttpDelete("{username}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteBasket([Required] string username)
    {
        var result = await _basketRepository.DeleteBasketFromUserName(username);
        return Ok(result);
    }

    [Route("[action]/{username}")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([Required] string username, [FromBody] BasketCheckout basketCheckout)
    {
        var basket = await _basketRepository.GetBasketByUserName(username);
        if (basket == null || !basket.Items.Any()) return NotFound();

        //publish checkout event to EventBus Message
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
        //await _publishEndpoint.Publish(eventMessage);

        var eventMessageJson = JsonSerializer.Serialize(eventMessage);

        // Produce the message to Kafka topic "basket-checkout-topic"
        var message = new Message<Null, string>
        {
            Value = eventMessageJson
        };

        // Send the message to Kafka
        await _kafkaProducer.ProduceAsync("basket-checkout-topic", message);

        return Accepted();
    }
}