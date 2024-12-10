using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersCategoriesController : ControllerBase
    {
        //call Repository
        private readonly IMembersCategoryRepository _repository;

        //DI - Dependency Injection
        public MembersCategoriesController(IMembersCategoryRepository repository)
        {
            _repository = repository;
        }

        #region   1 -  Get all members from DB - Search All
        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembers()
        {
            var members = await _repository.GetTblMembers();
            if (members == null)
            {
                return NotFound("No members found");
            }
            return Ok(members);
        }
        #endregion

        #region   2 - Get an Member based on Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMemberById(int id)
        {
            var members = await _repository.GetMembersById(id);
            if (members == null)
            {
                return NotFound("No member found");
            }
            return Ok(members);
        }
        #endregion

        #region   3  - Insert an Member -return Member record
        [HttpPost]
        public async Task<ActionResult<Member>> InsertMembersReturnRecord(Member member)
        {
            if (ModelState.IsValid)
            {
                //insert a new record and return as an object named employee
                var newMember = await _repository.PostTblMemberReturnRecord(member);
                if (newMember != null)
                {
                    return Ok(newMember);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region   4 - Insert an Member -return Id

        //[HttpPost("m1")]
        //public async Task<ActionResult<int>> InsertTblEmployeesReturnId(Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newMemberId = await _repository.PostTblMemberReturnId(member);
        //        if (newMemberId != null)
        //        {
        //            return Ok(newMemberId);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return BadRequest();
        //}
        #endregion

        #region    5  - Update an Member with ID
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateMembersReturnRecord(int id, Member member)
        {
            if (ModelState.IsValid)
            {
                var updateMember = await _repository.PutTblMember(id, member);
                if (updateMember != null)
                {
                    return Ok(updateMember);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  6  - Delete an Member
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            try
            {
                var result = _repository.DeleteMemberById(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Member could not be deleted or not found"
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

        #region    7  - Get all Categories
        [HttpGet("c2")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var cats = await _repository.GetTblCategories();
            if (cats == null)
            {
                return NotFound("No Category found");
            }
            return Ok(cats);
        }
        #endregion
    }
}
