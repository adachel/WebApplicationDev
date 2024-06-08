using HW4.Abstraction;
using HW4.DB;
using HW4.Models;
using System.Text;
using XSystem.Security.Cryptography;

namespace HW4.Repo
{
    public class UserRepo : IUserRepo
    {
        public void UserAdd(string name, string password, RoleId roleId)
        {
            using (var context = new UserContext())
            {
                if (roleId == RoleId.Admin)
                {
                    var admin = context.Roles.Count(x => x.RoleId == RoleId.Admin);
                    if (admin > 1)
                    {
                        throw new Exception("Администратор может быть только один");
                    }
                }

                var newUser = new User();
                newUser.Name = name;
                newUser.RoleId = roleId;

                newUser.Salt = new byte[16];
                new Random().NextBytes(newUser.Salt);
                var data = Encoding.UTF8.GetBytes(password).Concat(newUser.Salt).ToArray();

                newUser.Password = new SHA512Managed().ComputeHash(data);

                context.Add(newUser);
                context.SaveChanges();
            }
        }

        public RoleId UserCheck(string name, string password)
        {
            using (var context = new UserContext())
            {
                var checkUser = context.Users.FirstOrDefault(user => user.Name == name);
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

                throw new Exception("Some error");
            }

        }
    }
}
