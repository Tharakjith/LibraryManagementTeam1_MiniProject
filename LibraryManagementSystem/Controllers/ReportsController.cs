using LibraryManagementSystem.Repository;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        //Call repository
        private readonly IReportRepository _repository;

        //DI Constructor Injection
        public ReportsController(IReportRepository repository)
        {
            _repository = repository;
        }
        //// GET: api/<ReportsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ReportsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ReportsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReportsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReportsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}



        #region 1 - Get Reports of books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoAuCatViewModel>>> GetViewModelBooks()
        {
            var books = await _repository.GetViewModelBooks();
            if (books == null)
            {
                return NotFound("No Reports found");
            }

            return Ok(books);
        }

        #endregion


        #region 2- Get Reports of Borrowed books
        [HttpGet("vm")]
        public async Task<ActionResult<IEnumerable<BookBorrowViewModel>>> GetViewModelBookCategories()
        {
            var books = await _repository.GetViewModelBookCategories();
            if (books == null)
            {
                return NotFound("No Reports found");
            }

            return Ok(books);
        }

        #endregion

    }
}
