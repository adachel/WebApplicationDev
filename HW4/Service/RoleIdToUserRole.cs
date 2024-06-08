using HW4.DTO;
using HW4.Models;

namespace HW4.Service
{
    public class RoleIdToUserRole
    {
        public UserRole UserRoleToRoleId(RoleId id)
        {
            if (id == RoleId.Admin)
            {
                return UserRole.Admin;
            }
            else return UserRole.User;
        }
    }
}
