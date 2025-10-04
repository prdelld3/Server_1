using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

public static class Register
{
    public static void RegisterAuthor()
    {
        Console.Clear();
        using var context = new AppDbContext();

        Console.WriteLine("Whats the name of Author?");
        string newAuthor = Console.ReadLine();

        var author = new Author { Name = newAuthor };
        context.Authors.Add(author);
        context.SaveChanges();
    }

    public static void RegisterBook(Author author)
    {
        Console.Clear();
        using var context = new AppDbContext();

        Console.WriteLine("Whats the title of the Book?");
        string newBook = Console.ReadLine();

        var book = new Book { Title = newBook, Credits = new List<Credit>() };

        var credit = new Credit { AuthorId = author.AuthorId, Book = book };

        context.Credits.Add(credit);
        context.Books.Add(book);
        context.SaveChanges();
        Console.WriteLine($"Book '{newBook}' added to {author.Name}!");
    }

}

