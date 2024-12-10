using LibraryManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repository
{
    public interface IMembersCategoryRepository
    {
        #region 1 -  Get all members from DB - Search All
        public Task<ActionResult<IEnumerable<Member>>> GetTblMembers();
        #endregion

        #region   2 - Get an Member based on Id
        public Task<ActionResult<Member>> GetMembersById(int id);
        #endregion

        #region  3  - Insert an Member -return Member record
        public Task<ActionResult<Member>> PostTblMemberReturnRecord(Member member);
        #endregion

        #region    4 - Insert an Member -return Id
        public Task<ActionResult<int>> PostTblMemberReturnId(Member member);
        #endregion

        #region  5  - Update an Member with ID 
        public Task<ActionResult<Member>> PutTblMember(int id, Member member);
        #endregion

        #region  6  - Delete an Member
        public JsonResult DeleteMemberById(int id); 
        #endregion

        #region  7  - Get all Categories
        public Task<ActionResult<IEnumerable<Category>>> GetTblCategories();
        #endregion
    }
}
