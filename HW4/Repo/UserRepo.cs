using HW4.Abstraction;
using HW4.DB;
using HW4.Models;
using System.Text;
using XSystem.Security.Cryptography;

namespace HW4.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _userContext;

        public UserRepo(UserContext userContext)
        {
            _userContext = userContext;
        }

        public void UserAdd(string email, string password, RoleId roleId)
        {

            if (roleId == RoleId.Admin)
            {
                var admin = _userContext.Roles.Count(x => x.RoleId == RoleId.Admin);
                if (admin > 0)
                {
                    throw new Exception("There can only be one administrator");
                }
            }

            var user = _userContext.Users.FirstOrDefault(x => x.Email == email);    
            if (user == null)
            {
                var newUser = new User();
            newUser.Email = email;
            newUser.RoleId = roleId;

            newUser.Salt = new byte[16];
            new Random().NextBytes(newUser.Salt);
            var data = Encoding.UTF8.GetBytes(password).Concat(newUser.Salt).ToArray();

            newUser.Password = new SHA512Managed().ComputeHash(data);

            _userContext.Add(newUser);
            _userContext.SaveChanges();
            }
            else throw new Exception("This email is already in use");
        }

        public RoleId UserCheck(string email, string password)
        {
            
                var checkUser = _userContext.Users.FirstOrDefault(user => user.Email == email);
                if (checkUser == null)
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.UTF8.GetBytes(password).Concat(checkUser.Salt).ToArray();
                var hash = new SHA512Managed().ComputeHash(data);
                if (checkUser.Password.SequenceEqual(hash))
                {
                    return checkUser.RoleId;
                }

                throw new Exception("Incorrect password");
            

        }
    }
}
