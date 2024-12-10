using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        //DI -- constructor injection
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var employees = await _repository.GetAllBooks();
            if (employees == null)
            {
                return NotFound("No books found ");
            }
            return Ok(employees);
        }

        #region Search by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                return NotFound("No book found ");
            }
            return Ok(book);
        }
        #endregion
        #region 4 insert an orderitem return orderitems record
        [HttpPost]
        public async Task<ActionResult<Book>> postBookReturnRecord(Book bk)
        {
            if (ModelState.IsValid)
            {
                var newbook = await _repository.postBookReturnRecord(bk);
                if (newbook != null)
                {
                    return Ok(newbook);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region 5 insert an postbookReturnId return book by id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> postbookReturnId(Book bk)
        {
            if (ModelState.IsValid)
            {
                var newitemId = await _repository.postbookReturnId(bk);
                if (newitemId != null)
                {
                    return Ok(newitemId);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region update employee
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> putot(int id, Book bk)
        {
            if (ModelState.IsValid)
            {
                var updateitem = await _repository.putbook(id, bk);
                if (updateitem != null)
                {
                    return Ok(updateitem);
                }
                else
                {
                    return NotFound();
                }

            }
            return BadRequest();

        }
        #endregion
        #region  7  - Delete an Employee
        [HttpDelete("{id}")]
        public IActionResult Deletebook(int id)
        {
            try
            {
                var result = _repository.Deletebook(id);

                if (result == null)
                {
                 
                    return NotFound(new
                    {
                        success = false,
                        message = "book could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion
    }
}
