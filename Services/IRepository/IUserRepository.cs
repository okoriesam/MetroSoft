using Metrosoft.Models;

namespace Metrosoft.Services.IRepository
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        IEnumerable<Users> SearchUserByLastName(string LastName);
        IEnumerable<Users> GetAllUsers();

        bool EmailExist(string Email);
    }
}
