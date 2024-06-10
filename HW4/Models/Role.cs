using System.ComponentModel.DataAnnotations;

namespace HW4.Models
{
    public class Role
    {
        [Key]
        public RoleId RoleId { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
