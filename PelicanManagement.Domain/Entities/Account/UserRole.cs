using Shop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities.Account
{
    [Table("UserRole", Schema = "Acc")]

    public class UserRole   
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
