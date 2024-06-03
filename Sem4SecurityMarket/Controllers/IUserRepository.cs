using Sem4DTO;
using Sem4SecurityMarket.Model;

namespace Sem4SecurityMarket.Controllers
{
    public interface IUserRepository
    {
        public void AddUser(UserAutorizationRequest userAutorizationRequest);
        public RoleType CheckUser();
    }
}
