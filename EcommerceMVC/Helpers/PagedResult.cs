namespace EcommerceMVC.Helpers;

public class PagedResult<T> where T : class
{
    public int TotalItems { get; private set; }
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages => TotalItems / PageSize + (TotalItems % PageSize > 0 ? 1 : 0);
    public List<T> Items { get; private set; }

    public PagedResult(List<T> items, int totalItems, int currentPage, int pageSize = 10)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}