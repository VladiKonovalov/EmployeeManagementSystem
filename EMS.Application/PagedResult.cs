namespace EMS.Application
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PagedResult()
        {
        }

        public PagedResult(int totalCount, IEnumerable<T> items, int page, int pageSize)
        {
            TotalCount = totalCount;
            Items = items;
            Page = page;
            PageSize = pageSize;
        }
    }
}
