using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Basket;

public class BasketCheckoutDto
{
    private string _invoiceAddress;

    [Required] public string UserName { get; set; }

    public decimal TotalPrice { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] [EmailAddress] public string EmailAddress { get; set; }

    [Required] public string ShippingAddress { get; set; }

    public string? InvoiceAddress
    {
        get => _invoiceAddress;
        set => _invoiceAddress = value ?? ShippingAddress;
    }
}