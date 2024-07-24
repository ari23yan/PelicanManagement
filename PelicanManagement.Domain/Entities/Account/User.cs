using Shop.Domain.Entities.Common;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Account
{
    [Table("User", Schema = "Acc")]

    public class User : BaseEntity
    {
        [MaxLength(64)]
        public string? FirstName { get; set; }
        [MaxLength(64)]
        public string? LastName { get; set; }
        [MaxLength(64)]
        public string Username { get; set; }
        [MaxLength(128)]
        public string Password { get; set; }
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        [MaxLength(64)]
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public UserDetail UserDetail { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
