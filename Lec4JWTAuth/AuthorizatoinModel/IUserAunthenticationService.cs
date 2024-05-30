namespace Lec4JWTAuth.AuthorizatoinModel
{
    public interface IUserAuthenticationService
    {
        UserModel Authenticate(LoginModel model);
    }
}
