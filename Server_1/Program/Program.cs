using System;
using Microsoft.Data.SqlClient; 

static class Program
{
    static void Main()
    {
        Console.Clear();
        Console.WriteLine("Library Management System");
        
        //Propagate.PropogateLibrary();

        bool done = false;
        while (!done)
        {
            const string m =
            "\n 1. Register Author \n" +
            " 2. Edit Book or Author \n" +
            " 3. List Books that are in Library and are available\n" +
            " 4. Delete \n" +
            " 5. Borrow a Book \n" +
            " 6. See what books are lent out\n" +
            " 7. Reboot Library\n" +
        
            " 9. Exit";

            Console.WriteLine(m);

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    Register.RegisterAuthor();
                    break;

                case ConsoleKey.D2:
                    ReadData.EditAuthors();
                    break;

                case ConsoleKey.D3:
                    CheckoutBook.ListAvailableBooks();
                    break;

               case ConsoleKey.D4:
                    //CheckoutBook.DeleteBook();
                    break;

                 case ConsoleKey.D5:
                    CheckoutBook.BorrowBooks();
                    break;

                case ConsoleKey.D6:
                    CheckoutBook.ListAllLentOutBooks();
                    break;

                case ConsoleKey.D7:
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