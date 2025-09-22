using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient; 
class Program
{
    static void Main()
    {
        using var context = new AppDbContext();
        try
        {
            context.Database.OpenConnection();
            Console.WriteLine("✅ Connected to SQL Server!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Connection failed: " + ex.Message);
        }
    }
}