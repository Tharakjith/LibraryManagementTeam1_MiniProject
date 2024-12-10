using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Repository
{
    public interface IReportRepository
    {
        #region 1 - Get Reports of books
        //ViewModel
        public Task<ActionResult<IEnumerable<BoAuCatViewModel>>> GetViewModelBooks();
        #endregion

        //#region 2- Get Reports of Borrowed books
        ////ViewModel
        //public Task<ActionResult<IEnumerable<BoAuCatViewModel>>> GetViewModelBookCategories();
        //#endregion
    }
}
