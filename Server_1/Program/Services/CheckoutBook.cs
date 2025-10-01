using Microsoft.EntityFrameworkCore;

public static class CheckoutBook
{
    public static void ListAvailableBooks()
    {
        Console.Clear();
        using var context = new AppDbContext();

        var books = context.Books
            .Include(b => b.Loan)
            .Where(b => b.Loan == null)
            .ToList();

        if (!books.Any())
        {
            Console.WriteLine("No books found.");
            return;
        }

        Console.WriteLine("--- Available Books ---");

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Title}");
        }
        Console.ReadKey(intercept: true);
    }

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

    public static void BorrowBooks()
    {
        Console.Clear();
        using var context = new AppDbContext();

        var books = context.Books
            .Include(b => b.Loan)
            .Where(b => b.Loan == null)
            .ToList();

        if (!books.Any())
        {
            Console.WriteLine("No available books to lend out.");
            return;
        }

        Console.WriteLine("--- Available Books to Lend Out ---\n");
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Title}");
        }

        Console.Write("\nSelect a book number to lend out (or 0 to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= books.Count)
        {
            var selectedBook = books[choice - 1];
            selectedBook.Loan = new Loan
            {
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14)
            };
            context.SaveChanges();

            Console.WriteLine($"You have lent out '{selectedBook.Title}'.");
        }
        else
        {
            Console.WriteLine("Invalid selection or operation cancelled.");
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