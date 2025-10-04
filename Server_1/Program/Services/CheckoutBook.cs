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
        Console.WriteLine("No books are currently lent out.");
        return;
    }

    while (true)
    {
        Console.Clear();
        Console.WriteLine("--- Lent Out Books ---\n");

        for (int i = 0; i < books.Count; i++)
        {
            var authors = string.Join(", ", books[i].Credits.Select(c => c.Author?.Name));
            Console.WriteLine($"{i + 1}. {books[i].Title} by {authors} (Due: {books[i].Loan?.DueDate:yyyy-MM-dd})");
        }

        Console.Write("\nSelect a book number to return (or 0 to cancel): ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 0) break; 

            if (choice > 0 && choice <= books.Count)
            {
                var selectedBook = books[choice - 1];
                selectedBook.Loan = null;
                context.SaveChanges();
                Console.WriteLine($"Returned '{selectedBook.Title}'.");
                
            
                books = context.Books
                    .Include(b => b.Loan)
                    .Where(b => b.Loan != null)
                    .ToList();

                if (!books.Any())
                {
                    Console.WriteLine("\nAll books have been returned.");
                    break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(intercept: true);
            }
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Try again.");
        }
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