using UsersManagement.Domain.Entities.UsersManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.UsersManagement.Account
{
    [Table("RoleMenus", Schema = "Account")]

    public class RoleMenu : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public Role Role { get; set; }
        public Menu Menu { get; set; }
    }
}
