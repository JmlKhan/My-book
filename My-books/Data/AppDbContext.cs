using Microsoft.EntityFrameworkCore;

namespace My_books.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
