using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class ReportRepository : IReportRepository
    {
        //EF -VirtualDatabase
        private readonly LibraryMngtDbContext _context;

        //DI -- Constructor Injection
        public ReportRepository(LibraryMngtDbContext context)
        {
            _context = context;
        }
        #region 1 - Get Reports of books
        public async Task<ActionResult<IEnumerable<BoAuCatViewModel>>> GetViewModelBooks()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    return await (from b in _context.Books
                                  join c in _context.Categories on b.CategoryId equals c.CategoryId
                                  join a in _context.Authors on b.AuthorId equals a.AuthorId
                                  select new BoAuCatViewModel
                                  {
                                      BookId = b.BookId,
                                      Title = b.Title,
                                      AuthorName = a.AuthorName,
                                      CategoryName = c.CategoryName,
                                      AvailableCopies = b.AvailableCopies
                                  }).ToListAsync();

                }
                //Return an empty List if context is null
                return new List<BoAuCatViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 2- Get Reports of Borrowed books
        public async Task<ActionResult<IEnumerable<BookBorrowViewModel>>> GetViewModelBookCategories()
        {
            //LINQ
            try
            {
                if (_context != null)
                {
                    return await (from b in _context.Books
                                  join t in _context.BorrowTransactions on b.BookId equals t.BookId
                                  join a in _context.Authors on b.AuthorId equals a.AuthorId
                                  join c in _context.Categories on b.CategoryId equals c.CategoryId
                                  select new BookBorrowViewModel
                                  {
                                      Title = b.Title,
                                      AuthorName = a.AuthorName,
                                      CategoryName = c.CategoryName,
                                      AvailableCopies = b.AvailableCopies,
                                      price = b.Price,
                                      BorrowDate = t.BorrowDate,
                                      ReturnDate = t.ReturnDate,
                                      IsReturned = t.IsReturned
                                  }).ToListAsync();

                }
                //Return an empty List if context is null
                return new List<BookBorrowViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
