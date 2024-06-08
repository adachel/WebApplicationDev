namespace HW4.Abstraction
{
    public interface ITokenService
    {
        public string GenerateToken(string email, string roleName);
    }
}
