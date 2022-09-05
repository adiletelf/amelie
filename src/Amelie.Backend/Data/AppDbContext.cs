using Amelie.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Amelie.Backend.Data;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
