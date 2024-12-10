using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.Repository
{
    public interface ILoginRepository
    {
        public Task<LibLogin>ValidateUser(string username, string password);
    }
}
