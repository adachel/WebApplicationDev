using HW4.Models;

namespace HW4.Abstraction
{
    public interface IUserAuthService
    {
        public User UserAuth(Login login);
    }
}
