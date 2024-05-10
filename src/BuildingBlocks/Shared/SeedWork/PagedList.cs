namespace Shared.SeedWork;

public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> items, long totalItems, int pageIndex, int pageSize)
    {
        _metaData = new MetaData
        {
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = pageIndex,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
        AddRange(items);
    }

    private MetaData _metaData { get; }

    public MetaData GetMetaData()
    {
        return _metaData;
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> source,
        int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items =  source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, count, pageIndex, pageSize);
    }
}