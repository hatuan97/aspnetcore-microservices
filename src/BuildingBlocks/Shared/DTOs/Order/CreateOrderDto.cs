namespace Shared.DTOs.Order;

public class CreateOrderDto
{
    private string _invoiceAddress;
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }

    public string ShippingAddress { get; set; }

    public string? InvoiceAddress
    {
        get => _invoiceAddress;
        set => _invoiceAddress = value ?? ShippingAddress;
    }
}