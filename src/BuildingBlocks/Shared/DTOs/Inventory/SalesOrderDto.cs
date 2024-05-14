namespace Shared.DTOs.Inventory;

public class SalesOrderDto
{
    // Order's Document No
    public string GetOrderNo() => _orderNo;

    private string _orderNo;
    public void SetOrderNo(string orderNo)
    {
        _orderNo = orderNo;
    }

    public List<SaleItemDto> SaleItems { get; set; }
}