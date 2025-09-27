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
    /*public static void PropogateBooks()
    {
        Console.Clear();
        using var context = new AppDbContext();

        if (context.Books.Any()) return; // Already populated

        string[] titles =
        {
            "On the Seeds of Rebellion: The Propagation of Liberty",
            "Anarchism as Harmony: Against the Discord of Power",
            "The State and Its False Utopias",
            "The Muzzle of Authority: A Study of Censorship",
            "The Eyes of Tyranny: Surveillance and the Loss of Freedom",
            "Freedom in Chains: The State as Its Greatest Enemy",
            "Reality Without Masters: A Vision of Anarchist Society",
        };

        string[] authors =
        {
            "John Doe",
            "Jane Smith",
            "Alice Johnson",
            "John Doe"
        };

        for (int i = 0; i < titles.Length; i++)
        {
            var book = new Book
            {
                Title = titles[i],
                Credits = []
            };
            context.Books.Add(book);

            // save so IDs exist
            context.SaveChanges();

            var authorName = authors[i];
            var author = context.Authors.FirstOrDefault(a => a.Name == authorName);

            if (author == null)
            {
                author = new Author { Name = authorName, Credits = [] };
                context.Authors.Add(author);
                context.SaveChanges();
            }

            // Link via Credit
            var credit = new Credit
            {
                Book = book,
                Author = author
            };

            context.Credits.Add(credit);
            book.Credits.Add(credit);
            author.Credits.Add(credit);

            context.SaveChanges();
        }
    }*/
}