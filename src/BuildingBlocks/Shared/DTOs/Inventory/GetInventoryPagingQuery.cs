using Shared.SeedWork;

namespace Shared.DTOs.Inventory;

public class GetInventoryPagingQuery : PagingRequestParameters
{
    private string _itemNo;

    public string? SearchTerm { get; set; }

    public string ItemNo()
    {
        return _itemNo;
    }

    public void SetItemNo(string itemNo)
    {
        _itemNo = itemNo;
    }
}