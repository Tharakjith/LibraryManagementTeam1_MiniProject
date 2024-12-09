using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Model;

public partial class Member
{
    public int MemberId { get; set; }

    public string MemberName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; } = new List<BorrowTransaction>();

    public virtual ICollection<LibLogin> LibLogins { get; set; } = new List<LibLogin>();
}
