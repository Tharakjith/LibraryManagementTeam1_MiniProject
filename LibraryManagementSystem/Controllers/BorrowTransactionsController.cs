using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowTransactionsController : ControllerBase
    {
        private readonly IBorrowTransactionRepository _repository;
        //DI -- constructor injection
        public BorrowTransactionsController(IBorrowTransactionRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("{memberId}/{bookId}")]
        public async Task<IActionResult> BorrowBook(int memberId, int bookId)
        {
            try
            {
                // First, check if the book is available
                var book = await _repository.GetBookByIdAsync(bookId);

                // If book is not found or no available copies, return error
                if (book == null || book.AvailableCopies <= 0)
                {
                    return BadRequest("The book is not available for borrowing.");
                }

                // Execute the stored procedure to borrow the book
                await _repository.BorrowBookAsync(memberId, bookId);

                return Ok("Book borrowed successfully.");
            }
            catch (SqlException sqlEx)
            {
                // Catch specific SQL errors (e.g., stored procedure error)
                return BadRequest($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Generic exception handling
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("{transactionId}")]
        public async Task<IActionResult> ReturnBook(int transactionId)
        {
            try
            {
                await _repository.ReturnBookAsync(transactionId);
                return Ok("Book returned successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("history/{memberId}")]
        public async Task<IActionResult> GetBorrowingHistory(int memberId)
        {
            try
            {
                var history = await _repository.GetBorrowingHistoryAsync(memberId);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
