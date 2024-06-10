using HW4.DTO;
using HW4.Models;

namespace HW4.Service
{
    public class RoleIdToUserRole
    {
        public UserRole UserRoleRoleId(RoleId id)
        {
            if (id == RoleId.Admin)
            {
                return UserRole.Admin;
            }
            else return UserRole.User;
        }

        public RoleId RoleIdUserRole(UserRole id)
        {
            if (id == UserRole.Admin)
            {
                return RoleId.Admin;
            }
            else return RoleId.User;
        }
    }
}
