using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Pagination
{
    public static class QueryExtension
    {
        // extension method for pagination
        public static async Task<PageResult<T>> ToPageListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize)
        {
            var totalCount = await source.CountAsync();

            // get items for current page
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // return page result with items and pagination info
            return new PageResult<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
