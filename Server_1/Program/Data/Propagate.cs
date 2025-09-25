using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
public static class Propagate
{

    public static void PropogateAuthor()
    {
        Console.Clear();
        using var context = new AppDbContext();

        var author = new List<string>
        {
            "John Doe",
            "Jane Smith",
            "Alice Johnson"
        };
        author.ForEach(name =>
        {
            var existing = context.Authors.FirstOrDefault(a => a.Name == name);
            if (existing == null)
            {
                context.Authors.Add(new Author { Name = name });
            }
        });

        context.SaveChanges();
    }
}