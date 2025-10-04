using System;
using Microsoft.Data.SqlClient; 

static class Program
{
    static void Main()
    {
        Console.Clear();
        Console.WriteLine("Library Management System\n");

        bool done = false;
        while (!done)
        {
            const string m =
        
            " 1. Edit Book or Author \n" +
            " 2. List Books that are in Library and are available\n" +
            " 3. Borrow a Book \n" +
            " 4. See what books are lent out\n" +
            " 6. Reboot Library\n" +
        
            " 9. Exit";

            Console.WriteLine(m);

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    ReadData.EditAuthors();
                    break;

                case ConsoleKey.D2:
                    CheckoutBook.ListAvailableBooks();
                    break;

                 case ConsoleKey.D3:
                    CheckoutBook.BorrowBooks();
                    break;

                case ConsoleKey.D4:
                    CheckoutBook.ListAllLentOutBooks();
                    break;

                case ConsoleKey.D6:
                    Propagate.RebootLibrary();
                    Propagate.PropogateLibrary();
                    break;

                case ConsoleKey.D9:
                    done = true;
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            if (!done)
            {
                Console.WriteLine("Press any key to continue");
                _ = Console.ReadKey(intercept: true);
                Console.Clear();
            }
        }
    }
}