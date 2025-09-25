using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AppDbContext : DbContext
{

    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer
            ("Server=localhost,1433;Database=Ministry;User Id=sa;Password=.nGzz8tqt9;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
        .HasIndex(a => a.Name)
        .IsUnique();
    }
}

  public class Author
{
    public int AuthorId { get; set; }
    public string? Name { get; set; }
    
}