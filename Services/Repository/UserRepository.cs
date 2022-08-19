using Metrosoft.Context;
using Metrosoft.Models;
using Metrosoft.Services.IRepository;

namespace Metrosoft.Services.Repository
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {

        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public IEnumerable<Users> GetAllUsers()
        {
            var query = _context.Users;

            return query.ToList();
        }

        public bool EmailExist(string Email)
        {
            var emailExist = _context.Users.Where(x=>x.Email == Email).FirstOrDefault();
            if (emailExist != null)
                return true;
            return false;
        }

        public IEnumerable<Users> SearchUserByLastName(string LastName)
        {
            var UsersByEmail = _context.Users.Where(u => u.LastName.Contains(LastName)).ToList();
            if (UsersByEmail != null)
            {
                return UsersByEmail;
            }
            return null;
        }

        
    }
}
