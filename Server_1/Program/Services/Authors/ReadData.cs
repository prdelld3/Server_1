using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
public class ReadData
{
    public static void EditAuthors()
    {
        while (true)
        {
            Console.Clear();
            using var context = new AppDbContext();

            if (!context.Authors.Any())
            {
                Console.WriteLine("No authors found.");
                return;
            }
            var authors = context.Authors.OrderBy(a => a.Name).ToList();

            Console.WriteLine("--- Authors ---\n");
            for (int i = 0; i < authors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {authors[i].Name}");
            }
            Console.WriteLine("\n0. Exit author menu");
            Console.Write("\nChoose an author: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0) break;

                if (choice > 0 && choice <= authors.Count)
                {
                    var selectedAuthor = authors[choice - 1];

                    var books = context.Credits
                        .Where(c => c.AuthorId == selectedAuthor.AuthorId)
                        .Include(c => c.Book)
                        .Select(c => c.Book.Title)
                        .ToList();

                    Console.Clear();
                    Console.WriteLine($"--- Books by {selectedAuthor.Name} ---\n");

                    if (books.Any())
                    {
                        foreach (var book in books)
                        {
                            Console.WriteLine($"- {book}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found for this author.");
                        Register.RegisterBook(selectedAuthor);

                    }
                    Console.WriteLine("\n 'Add' To add a new book for this author");
                    Console.WriteLine(" 'Delete' To delete this author (and all their books)");

                    var input = Console.ReadLine()?.Trim();

                    if (input.Equals("Add", StringComparison.OrdinalIgnoreCase))
                    {
                        Register.RegisterBook(selectedAuthor);
                        continue;
                    }
                    else if (input.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                    {
                        /*var credits = context.Credits.Where(c => c.AuthorId == selectedAuthor.AuthorId).ToList();
                        context.Credits.RemoveRange(credits);
                        context.Authors.Remove(selectedAuthor);
                        context.SaveChanges();
                        Console.WriteLine($"Author {selectedAuthor.Name} and their books have been deleted.");
                        continue;
                        */
                        DeleteBook(selectedAuthor);
                    }
                }

                Console.WriteLine("\nPress any key to go back to authors...");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Invalid input. Press any key to try again...");
                Console.ReadKey();
            }
        }
    }

    private static void DeleteBook(Author selectedAuthor)
    {
        Console.Clear();
        using var context = new AppDbContext();

        /*if (!context.Books.Any())
        {
            Console.WriteLine("No books found.");
            return;
        }
        var books = context.Credits
       .Where(c => c.AuthorId == selectedAuthor.AuthorId)
       .Include(c => c.Book)
       .Select(c => c.Book)
       .Distinct()
       .OrderBy(b => b.Title)
       .ToList();

        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title}");
        }*/
        
        var books = context.Books.OrderBy(b => b.Title).ToList();

            Console.WriteLine("--- Books ---\n");
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {books[i].Title}");
            }
            Console.WriteLine("\n0. Exit book menu");

            Console.Write("\nChoose a book to delete: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Wamsk");
                if (choice == 0) return;

                if (choice > 0 && choice <= books.Count)
                {
                    var selectedBook = books[choice - 1];
                    var credits = context.Credits.Where(c => c.BookId == selectedBook.BookId).ToList();
                    context.Credits.RemoveRange(credits);
                    context.Books.Remove(selectedBook);
                    context.SaveChanges();
                    Console.WriteLine($"Book '{selectedBook.Title}' has been deleted."); 
                }
            }
    }
    
    public static void DeleteAuthor()
    {
        Console.Clear();
        using var context = new AppDbContext();

        if (!context.Authors.Any())
        {
            Console.WriteLine("No authors found.");
            return;
        }
        var authors = context.Authors.OrderBy(a => a.Name).ToList();

        Console.WriteLine("--- Authors ---\n");
        for (int i = 0; i < authors.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {authors[i].Name}");
        }
        Console.WriteLine("\n0. Exit author menu");

        Console.Write("\nChoose an author to delete: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 0) return;

            if (choice > 0 && choice <= authors.Count)
            {
                var selectedAuthor = authors[choice - 1];

                var credits = context.Credits.Where(c => c.AuthorId == selectedAuthor.AuthorId).ToList();
                context.Credits.RemoveRange(credits);
                context.Authors.Remove(selectedAuthor);
                context.SaveChanges();
                Console.WriteLine($"Author {selectedAuthor.Name} and their books have been deleted.");
            }
        }
    }
}

