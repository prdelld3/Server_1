using System;
using Microsoft.Data.SqlClient; 

static class Program
{
    static void Main()
    {
        //Propagate.PropogateAuthor();
        ReadData.ShowAuthors();
        bool done = false;
        while (!done)
        {
            const string m =
            "\n 1. Add a Book\n" +
            " 2. List Authors and their books \n" +
            " 3. List Books that are in Library and are available\n" +
            " 4. Delete \n" +
            " 5. Borrow a Book \n" +
            " 6. See what books are lent out\n" +
            " 7. Reboot Library\n" +
            " 8. Edit Book or Author\n" +
            " 9. Exit";

            Console.WriteLine(m);

            switch (Console.ReadKey(intercept: true).Key)
            {
                case ConsoleKey.D1:
                    //AddData.AddBookAndAuthor();
                    Register.RegisterBook();
                    break;

                case ConsoleKey.D2:
                    ReadData.ShowAuthors();
                    break;

                /*case ConsoleKey.D3:
                    ReadData.ListAvailableBooks();
                    break;

                case ConsoleKey.D4:
                    CheckoutBook.ReturnAllBooks();
                    break;

                case ConsoleKey.D5:
                    CheckoutBook.LentOutAllBooksBooks();
                    break;

                case ConsoleKey.D6:
                    CheckoutBook.ShowBorrowedBooks();
                    break;*/

                case ConsoleKey.D7:
                    
                    Propagate.PropogateAuthor();
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