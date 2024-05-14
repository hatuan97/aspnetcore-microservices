using Shared.Enums.Inventory;

namespace Shared.DTOs.Inventory;

public record SalesProductDto(string ExternalDocumentNo, int Quantity)
{
    public EDocumentType DocumentType = EDocumentType.Sale;
    
    private string _itemNo;

    public string GetItemNo() => _itemNo;

    public void SetItemNo(string itemNo)
    {
        _itemNo = itemNo;
    }
}