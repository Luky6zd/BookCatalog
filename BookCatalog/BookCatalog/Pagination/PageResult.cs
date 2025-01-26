
namespace BookCatalog.Pagination
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Items1 { get; }
        public int PageNumber { get; }

        public PageResult(IEnumerable<T> items, int totalCount, int totalPages, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PageResult(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items1 = items;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
