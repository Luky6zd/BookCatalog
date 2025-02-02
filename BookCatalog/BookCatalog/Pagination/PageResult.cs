
using BookCatalog.Models;

namespace BookCatalog.Pagination
{
    // generic pagination model
    public class PageResult<T>
    {
        // items for current page
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Book> Books { get; }
        public int TotalBooks { get; }
        public int PageNumber { get; }

        // constructors
        public PageResult(IEnumerable<T> items, int totalCount, int totalPages, int currentPage, int pageSize)
        {
            // avoid null reference exception by initializing empty List
            Items = items ?? new List<T>();
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PageResult(List<Book> books, int totalBooks, int pageNumber, int pageSize)
        {
            Books = books;
            TotalBooks = totalBooks;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PageResult(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PageResult(List<Book> books, int pageNumber, int pageSize)
        {
            Books = books;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
