using Lec4JWTAuth.DB;
using System.Security.Cryptography;
using System.Text;

namespace Lec4JWTAuth.Repo
{
    public class UserRepository : IUserRepository
    {
        public void UserAdd(string name, string password, RoleId roleId)
        {
            using (var context = new UserContext())
            { 
                if (roleId == RoleId.Admin) 
                {
                    var c = context.Users.Count(x => x.RoleId == RoleId.Admin);
                    if (c > 0) 
                    {
                        throw new Exception("Администратор может быть только один");
                    }
                }
                var user = new User();
                user.Name = name;
                user.RoleId = roleId;

                user.Salt = new byte[16];  // создаем массив Salt
                new Random().NextBytes(user.Salt); // заполняем массив случайными значениями

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray(); // преобр пароль в массив байт и конкатенируем с Salt

                SHA512 shaM = new SHA512Managed(); // создаем объект SHA512

                user.Password = shaM.ComputeHash(data); // вычисляем hash-функцию пароля и соли. В поле Password записываем значение hash

                context.Add(user);  
                context.SaveChanges();
            }
        }

        public RoleId UserCheck(string name, string password)
        {
            using (var context = new UserContext())
            { 
                var user = context.Users.FirstOrDefault(x => x.Name == name);  // получаем пользователя

                if (user == null) // если такого пользователя нет
                {
                    throw new Exception("User not found");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var bpassword = shaM.ComputeHash(data); // расчитываем hash

                if(user.Password.SequenceEqual(bpassword)) // сравриваем с hash из бд. SequenceEqual - побайтно сравнивает две последовательности значений
                {
                    return user.RoleId;
                }
                else 
                {
                    throw new Exception("Wrong password");
                }
            }
        }
    }
}
