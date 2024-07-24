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
    [Table("UserDetail", Schema = "Acc")]

    public class UserDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public GenderType? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLoginDate { get; set; }
        [MaxLength(256)]
        public string ProfilePicture { get; set; }
    }
}
