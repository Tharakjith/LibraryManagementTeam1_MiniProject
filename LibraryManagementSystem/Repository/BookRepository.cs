using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class BookRepository : IBookRepository

    {
        private readonly LibraryMngtDbContext _context;
        //DI --constructor injection
        //field injection(one of the types)
        //constructor name 
        // context  
        public BookRepository(LibraryMngtDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            if (_context != null)
            {
                return await _context.Books
                    .Include(b => b.Category)
                    .Include(b => b.Author)
                    .ToListAsync();
            }

            // Return an empty list if context is null
            return new List<Book>();
        }

        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            try
            {
                if (_context != null)
                {
                    // find the employee by id 
                    var book = await _context.Books
                    .Include(book => book.Category)
                     .Include(book => book.Author)
                    .FirstOrDefaultAsync(e => e.BookId == id);
                    return book;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<Book>> postBookReturnRecord(Book book)
        {
            try
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book), "book data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                await _context.Books.AddAsync(book);


                await _context.SaveChangesAsync();

                var lib = await _context.Books.Include(e => e.Category).Include(e => e.Author)
                    .FirstOrDefaultAsync(e => e.BookId == book.BookId);
                return lib;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActionResult<int>> postbookReturnId(Book book)
        {
            try
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book), "book data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                await _context.Books.AddAsync(book);


                var changesRecord = await _context.SaveChangesAsync();
                if (changesRecord > 0)
                {
                    return book.BookId;
                }
                else
                {
                    throw new Exception("failed to save book record to the database");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async  Task<ActionResult<Book>> putbook(int id, Book book)
        {
            try
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book), "book data is null");

                }

                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized.");
                }

                var existingOrderItem = await _context.Books.FindAsync(id);
                if (existingOrderItem == null)
                {
                    return null;
                }


                existingOrderItem.TotalCopies = book.TotalCopies;
                existingOrderItem.AvailableCopies = book.AvailableCopies;
                existingOrderItem.Price = book.Price;
                




                await _context.SaveChangesAsync();

                var Bk = await _context.Books.Include(e => e.Category).Include(e => e.Author)
                    .FirstOrDefaultAsync(e => e.BookId == book.BookId);
                return Bk;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult Deletebook(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid book id"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }
                // ensure context is not null
                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database coontext is not initialized"

                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                //Find the employee by id
                var existingbook = _context.Books.Find(id);
                if (existingbook == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Book  not found"

                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                //remove

                _context.Books.Remove(existingbook);



                //save changes to the database
                _context.SaveChangesAsync();


                return new JsonResult(new
                {
                    success = true,
                    message = "Book deleted successfully"

                })
                {
                    StatusCode = StatusCodes.Status200OK
                };

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database coontext is not initialized"

                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
    
}
 