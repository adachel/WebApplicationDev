namespace Sem4SecurityMarket.Abstraction
{
    public interface ITokenService
    {
        public string GenerateToken(string email, string roleName);
    }
}
