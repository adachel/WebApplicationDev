namespace Lec4JWTAuth.AuthorizatoinModel
{
    public class AuthenticationMock : IUserAuthenticationService
    {
        public UserModel Authenticate(LoginModel model)
        {
            if (model.Name == "admin" && model.Password == "password")
            {
                return new UserModel { Password = model.Password, UserName = model.Name, Role = UserRole.Administrator }; 
            }
            if (model.Name == "user" && model.Password == "super")
            {
                return new UserModel { Password = model.Password, UserName = model.Name, Role = UserRole.User };
            }
            return null;
        }
    }
}
