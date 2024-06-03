using Lec4JWTAuth.DB;

namespace Lec4JWTAuth.Repo
{
    public interface IUserRepository
    {
        public void UserAdd(string name, string password, RoleId roleId);
        public RoleId UserCheck(string name, string password);
    }
}
