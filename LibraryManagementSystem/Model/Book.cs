using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Model;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? AuthorId { get; set; }

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public decimal Price { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();

    public virtual Category? Category { get; set; }
}
