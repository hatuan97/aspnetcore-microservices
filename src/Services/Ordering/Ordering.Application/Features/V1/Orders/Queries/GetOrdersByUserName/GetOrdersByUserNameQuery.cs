using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders;

public class GetOrdersByUserNameQuery : IRequest<ApiResult<List<OrderDto>>>
{
    public GetOrdersByUserNameQuery(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }

    public string UserName { get; set; }
}