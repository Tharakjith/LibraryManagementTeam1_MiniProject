using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repository
{
    public interface IBorrowTransactionRepository
    {

        Task BorrowBookAsync(int memberId, int bookId);

        // Method to return a book
        Task ReturnBookAsync(int transactionId);

        // Method to get borrowing history
        Task<IEnumerable<BorrowTransaction>> GetBorrowingHistoryAsync(int memberId);

        // Add method to get a book by its ID
        Task<Book> GetBookByIdAsync(int bookId);


    }
}
