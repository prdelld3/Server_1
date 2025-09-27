using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

public static class Register
{
    public static void RegisterBook()
    {
        Console.Clear();
        using var context = new AppDbContext();

        Console.WriteLine("Whats the name of Author?");
        string newAuthor = Console.ReadLine();

        var author = new Author { Name = newAuthor };
        context.Authors.Add(author);
        context.SaveChanges();
    }
    
        
}