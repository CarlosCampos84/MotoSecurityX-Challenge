namespace CP4.MotoSecurityX.Application.Common;

public sealed class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; }
    public int Total { get; }
    public int Page { get; }
    public int PageSize { get; }
    public PagedLinks Links { get; }

    public PagedResult(
        IReadOnlyList<T> items,
        int total,
        int page,
        int pageSize,
        PagedLinks links)
    {
        Items = items;
        Total = total;
        Page = page;
        PageSize = pageSize;
        Links = links;
    }

    // Fábrica usada pelos handlers
    public static PagedResult<T> Create(
        IEnumerable<T> items,
        int total,
        int page,
        int pageSize,
        Func<int, int, string> linkFactory)
    {
        var list = items.ToList();
        var lastPage = Math.Max(1, (int)Math.Ceiling((double)total / Math.Max(1, pageSize)));

        // usando argumentos POSICIONAIS para evitar erro de nome do parâmetro
        var links = new PagedLinks(
            linkFactory(page, pageSize),                         // self
            page < lastPage ? linkFactory(page + 1, pageSize) : null, // next
            page > 1 ? linkFactory(page - 1, pageSize) : null,        // prev
            linkFactory(1, pageSize),                           // first
            linkFactory(lastPage, pageSize)                     // last
        );

        return new PagedResult<T>(list, total, page, pageSize, links);
    }
}

public sealed record PagedLinks(
    string Self,
    string? Next,
    string? Prev,
    string First,
    string Last);