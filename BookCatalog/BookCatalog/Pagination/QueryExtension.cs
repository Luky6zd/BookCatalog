using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Pagination
{
    public static class QueryExtension
    {
        public static async Task<PageResult<T>> ToPageListAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize)
        {
            var totalCount = await source.CountAsync();

            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
