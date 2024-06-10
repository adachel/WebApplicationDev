using HW4.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HW4.Controllers
{
    public class RestrictedController : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.UserRole}");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Admin, User")]
        public ActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are an {currentUser.UserRole}");
        }

        private UserDTO GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;

                var role = (UserRole)Enum.Parse( typeof(UserRole), userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value );

                return new UserDTO{ Email = email, UserRole = role };
            }
            return null!;
        }
    }
}
