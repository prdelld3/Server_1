using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
public class ReadData
{
    public static void ShowAuthors()
    {
        Console.Clear();
        using var context = new AppDbContext();
        if(!context.Authors.Any())
        {
            Console.WriteLine("No authors found.");
            return;
        }
        var authors = context.Authors.OrderBy(a => a.Name).ToList();

        Console.WriteLine("=== Authors ===\n");

        foreach (var author in authors)
        {
            Console.WriteLine($" {author.Name} ");
        }
    }
}

