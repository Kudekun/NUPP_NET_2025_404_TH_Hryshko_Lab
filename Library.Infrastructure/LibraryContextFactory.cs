using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Library.Infrastructure;

public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
{
    public LibraryContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlite("Data Source=library.db")
            .Options;
        return new LibraryContext(options);
    }
}
