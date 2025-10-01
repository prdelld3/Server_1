
public class Author
{
    public int AuthorId { get; set; }
    public string? Name { get; set; }
    public List<Credit> Credits { get; set; } = new();
}

public class Book
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public Loan? Loan { get; set; }
    public List<Credit> Credits { get; set; } = new();
}

public class Credit
{
    public int CreditId { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}

public class Loan
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? DueDate { get; set; }
}