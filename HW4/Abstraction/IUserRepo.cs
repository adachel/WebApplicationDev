using HW4.Models;

namespace HW4.Abstraction
{
    public interface IUserRepo
    {
        public void UserAdd(string name, string password, RoleId roleId);
        public RoleId UserCheck(string name, string password);
    }
}
