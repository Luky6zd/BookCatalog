using BookCatalog.Models;
using BookCatalog.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog
{
    // configuring Sql database
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookExample> BookExamples { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<PaginationModel> paginationModels { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

    }
}
