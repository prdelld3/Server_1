using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
public static class Propagate
{
    public static void RebootLibrary()
    {
        Console.Clear();
        using var context = new AppDbContext();

        context.Credits.RemoveRange(context.Credits);
        context.Books.RemoveRange(context.Books);
        context.Authors.RemoveRange(context.Authors);
        context.SaveChanges();
    }
    public static void PropogateLibrary()
    {
        RebootLibrary();
        
        Console.Clear();
        using var context = new AppDbContext();

        if (context.Books.Any()) return; 

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
                Loan = null,
                Credits = new List<Credit>(),
                //LoanHistories = new List<LoanHistory>()
            };
            context.Books.Add(book);

            // cause ID needs to exist!
            context.SaveChanges();

            var authorName = authors[i];
            var author = context.Authors.FirstOrDefault(a => a.Name == authorName);

            if (author == null)
            {
                author = new Author { Name = authorName, Credits = [] };
                context.Authors.Add(author);
                context.SaveChanges();
            }

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
    }
}