using UsersManagement.Domain.Entities.UsersManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.UsersManagement.Account
{
    [Table("Users", Schema = "Account")]

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
        [MaxLength(128)]
        public string? Email { get; set; }
        [MaxLength(6)]
        public string? OtpCode { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
