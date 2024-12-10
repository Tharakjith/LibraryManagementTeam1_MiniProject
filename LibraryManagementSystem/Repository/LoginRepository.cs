using LibraryManagementSystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repository
{
    public class LoginRepository:ILoginRepository
    {
        //Virtual Dbcontext
        private readonly LibraryMngtDbContext _context;

        //DI
        public LoginRepository(LibraryMngtDbContext context)
        {
            _context = context;
        }


        #region Validate User
        public async Task<LibLogin> ValidateUser(string username, string password)
        {
            try
            {
                if (_context != null)
                {
                    LibLogin dbUser = await _context.LibLogins.FirstOrDefaultAsync(
                        u => u.Username == username && u.Upassword == password);
                    if (dbUser != null)
                    {
                        return dbUser;
                    }
                }
                //Return an empty if _context is null
                return null;
            }
            catch (Exception ex)
            {
                //return StatusCode(500, $"Internal server error: { ex.Message}"); //
                return null;
            }
        }



        #endregion
        //Generate JWT Token
    }
}
