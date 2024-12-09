using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Model;

public partial class LibLogin
{
    public int LoginId { get; set; }

    public int? MemberId { get; set; }

    public string Username { get; set; } = null!;

    public string Upassword { get; set; } = null!;

    public virtual Member? Member { get; set; }
}
