using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient; 


public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         optionsBuilder.UseSqlServer
         ("Server=localhost,1433;Database=master;User Id=sa;Password=.nGzz8tqt9;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

       
        // modelBuilder.Entity<User>(entity =>
        // {
        //     entity.HasKey(e => e.Id);
        //     entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
        //     entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        // });

        /*public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }*/
    }
    
}