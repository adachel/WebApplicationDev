namespace Lec4JWTAuth.AuthorizatoinModel
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
