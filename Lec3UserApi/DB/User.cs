using System.ComponentModel.Design;
using System.Data;

namespace Lec3UserApi.DB
{
    public class User
    {
        public Guid Id { get; set; } // тип для идентифик в комп системах, уникален в пределах всей вселенной
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Registered {  get; set; } = DateTime.Now; // дата регистрации
        public bool Active { get; set; } = true; // действительность пользователя

    }
}
