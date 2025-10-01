using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer
            ("Server=localhost,1433;Database=Ministry;User Id=sa;Password=.nGzz8tqt9;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title)
            .IsUnique();

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Credit>()
            .HasIndex(c => new { c.BookId, c.AuthorId })
            .IsUnique();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Credits)
            .WithOne(c => c.Book)
            .HasForeignKey(c => c.BookId)
            .IsRequired();

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Credits)
            .WithOne(c => c.Author)
            .HasForeignKey(c => c.AuthorId)
            .IsRequired();

            modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithOne(b => b.Loan)
            .HasForeignKey<Loan>(l => l.BookId)
            .IsRequired();
    }
}

