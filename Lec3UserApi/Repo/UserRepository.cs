using AutoMapper;
using Lec3UserApi.DB;
using Lec3UserApi.DTO;

namespace Lec3UserApi.Repo
{
    public class UserRepository : IUserRepository
    {
        private IMapper _mapper;
        private AppDBContext _context;

        public UserRepository(IMapper mapper, AppDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddUser(UserDTO user)
        {
            using (_context)
            { 
                var userDB = _mapper.Map<User>(user);
                _context.Users.Add(userDB);
                _context.SaveChanges();
            }
        }

        public bool Exist(string email)
        {
            using (_context)
            { 
                return _context.Users.Any(x => x.Active && x.Email == email);
            }
        }

        public bool Exist(Guid id)
        {
            using (_context)
            {
                return _context.Users.Any(x => x.Active && x.Id == id);
            }
        }
    }
}
