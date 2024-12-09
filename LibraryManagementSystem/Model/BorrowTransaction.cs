using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Model;

public partial class BorrowTransaction
{
    public int TransactionId { get; set; }

    public int? MemberId { get; set; }

    public int? BookId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsReturned { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Member? Member { get; set; }
}
