using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class BookContext: DbContext
    {
        public BookContext(){}
        public BookContext(DbContextOptions<BookContext> options):base(options){}

        public DbSet<Book> Books { get; set; }

    }
}
