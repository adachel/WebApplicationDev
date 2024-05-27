using Lec3UserApi.DTO;

namespace Lec3UserApi.Repo
{
    public interface IUserRepository
    {
        public void AddUser(UserDTO user);
        public bool Exist(string email);
        public bool Exist(Guid id);

    }
}
