using Sem4DTO;
using Sem4SecurityMarket.Context;
using Sem4SecurityMarket.Controllers;
using Sem4SecurityMarket.Model;

namespace Sem4SecurityMarket.Repository
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(UserAutorizationRequest userAutorizationRequest)
        {
            using (var context = new AuthContext())
            {
                var checkUser = context.Users.Any(e => e.Email == userAutorizationRequest.Email);

                //switch (userAutorizationRequest.UserRole)
                //{ }

                //if (!checkUser)
                //{
                //    new User() { Email = userAutorizationRequest.Email,  };
                //}

            }
        }

        public RoleType CheckUser()
        {
            throw new NotImplementedException();
        }
    }
}
