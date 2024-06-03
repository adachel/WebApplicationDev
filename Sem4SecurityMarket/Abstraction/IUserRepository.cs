using Sem4DTO;
using Sem4SecurityMarket.Model;

namespace Sem4SecurityMarket.Abstraction
{
    public interface IUserRepository
    {
        public void AddUser(string email, string password, UserRoleType userRoleType);
        public UserRoleType CheckUser(string email, string password);
    }
}
