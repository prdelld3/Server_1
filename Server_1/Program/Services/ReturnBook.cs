using Microsoft.EntityFrameworkCore;

public static class ReturnBook
{
    public static void ListAllLentOutBooks()
    {
        Console.Clear();
        using var context = new AppDbContext();
        var books = context.Books
            .Include(b => b.Loan)
            .Where(b => b.Loan != null)
            .ToList();

        if (!books.Any())
        {
            Console.WriteLine("No books found.");
            return;
        }

        Console.WriteLine("--- Lent Out Books ---\n");
        foreach (var book in books)
        {
            var authors = string.Join(", ", book.Credits.Select(c => c.Author?.Name));
            Console.WriteLine($"=== {book.Title} by {authors} (Due: {book.Loan.DueDate}) ===");
        }
        if (ConsoleKey.D1 == Console.ReadKey(intercept: true).Key)
        {
            ReturnAllBooks();
        }
    }

    public static void ReturnAllBooks()
    {
        using var context = new AppDbContext();
        var lentOutBooks = context.Books
            .Include(b => b.Loan)
            .Where(b => b.Loan != null)
            .ToList();

        if (!lentOutBooks.Any())
        {
            Console.WriteLine("No books are currently lent out.");
            return;
        }
        Console.WriteLine("--- Returned Books ---");
        for (int i = 0; i < lentOutBooks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {lentOutBooks[i].Title}");
            lentOutBooks[i].Loan = null;
        }
        context.SaveChanges();
    }
}