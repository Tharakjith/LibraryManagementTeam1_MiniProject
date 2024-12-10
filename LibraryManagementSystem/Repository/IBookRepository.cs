using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repository
{
    public interface IBookRepository
    {
        #region 1-Get all Books
        public Task<ActionResult<IEnumerable<Book>>> GetAllBooks();

        #endregion
        
        #region 2-Get employees by id 
        public Task<ActionResult<Book>> GetBookById(int id);
        #endregion
        #region  3-insert Book
        public Task<ActionResult<Book>> postBookReturnRecord(Book book);
        #endregion
        #region 4 insert all records
        public Task<ActionResult<int>> postbookReturnId(Book book);
        #endregion 
        #region 6 get Book by its id
        public Task<ActionResult<Book>> putbook(int id, Book book);
        #endregion
        #region 7-delete employee
        public JsonResult Deletebook(int id);
        #endregion
    }
}
