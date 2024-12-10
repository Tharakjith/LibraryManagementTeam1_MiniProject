using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Model;

public partial class LibraryMngtDbContext : DbContext
{
    public LibraryMngtDbContext()
    {
    }

    public LibraryMngtDbContext(DbContextOptions<LibraryMngtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowTransaction> BorrowTransactions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<LibLogin> LibLogins { get; set; }

    public virtual DbSet<Member> Members { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =LAPTOP-1D8H5N1A\\SQLEXPRESS; Initial Catalog = LibraryMngtDb; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC3445E175F7");

            entity.Property(e => e.AuthorName).HasMaxLength(255);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207EEE03E1D");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorId__403A8C7D");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Books__CategoryI__3F466844");
        });

        modelBuilder.Entity<BorrowTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__BorrowTr__55433A6BDA57DF99");

            entity.Property(e => e.BorrowDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowTransactions)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BorrowTra__BookI__440B1D61");

            entity.HasOne(d => d.Member).WithMany(p => p.BorrowTransactions)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__BorrowTra__Membe__4316F928");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B1AF0FF05");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0C4B2072D").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<LibLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__LibLogin__4DDA28188F781D3D");

            entity.ToTable("LibLogin");

            entity.HasIndex(e => e.Username, "UQ__LibLogin__536C85E415D87EAA").IsUnique();

            entity.Property(e => e.Upassword)
                .HasMaxLength(255)
                .HasColumnName("UPassword");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.LibLogins)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__LibLogin__Member__48CFD27E");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B18C4136F4D");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Members__85FB4E3828FF00BF").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Members__A9D10534B13B94BA").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.MemberName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
