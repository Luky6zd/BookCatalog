using BookCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog
{
    // configuring Sql database
    public class DataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builderDB)
        {
            builderDB.UseSqlServer("Data Source=localhost; Initial Catalog=bookcatalog; Integrated Security=true; TrustServerCertificate=true");
        }
    }
}
