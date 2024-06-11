﻿using System.ComponentModel.DataAnnotations;

namespace Sem4SecurityMarket.Model
{
    public class Role
    {
        [Key]
        public UserRoleType RoleId { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
