using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Design;
using static LibraryManagementSystem.Repository.BorrowTransactionRepository;

namespace LibraryManagementSystem.Repository
{


    public class BorrowTransactionRepository : IBorrowTransactionRepository
    {
        //EF - Virtual database
        private readonly LibraryMngtDbContext _context;

        //Dependency Injection - constructor injection - to get all the resources from the DBContext.
        //otherwise , everytime we need a table or data, we need to create a object for everything. ie)25 tables, then 25 objects need to be created.
        public BorrowTransactionRepository(LibraryMngtDbContext context)
        {
            _context = context; //_context - virtual
        }

        public async Task BorrowBookAsync(int memberId, int bookId)
        {
            var sql = "EXEC BorrowBook @MemberId = @MemberId, @BookId = @BookId";
            try
            {
                await _context.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@MemberId", memberId),
                    new SqlParameter("@BookId", bookId));
            }
            catch (Exception ex)
            {
                // Capture the exception message for debugging purposes
                throw new Exception($"Error during BorrowBook execution: {ex.Message}");
            }
        }

        // Execute the stored procedure to return a book
        public async Task ReturnBookAsync(int transactionId)
        {
            var sql = "EXEC ReturnBook @TransactionId = @TransactionId";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@TransactionId", transactionId));
        }

        // Get borrowing history of a member
        public async Task<IEnumerable<BorrowTransaction>> GetBorrowingHistoryAsync(int memberId)
        {
            var history = await _context.BorrowTransactions
                .Where(bt => bt.MemberId == memberId)
                .ToListAsync();

            return history;
        }

        // Implement the GetBookByIdAsync method
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.BookId == bookId);
        }
    }
}

            
                            
    
        
   

