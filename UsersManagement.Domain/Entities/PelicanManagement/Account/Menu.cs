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
    [Table("Menus", Schema = "Account")]

    public class Menu : BaseEntity
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string? Name { get; set; }
        [MaxLength(256)]
        public string? Name_Farsi { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public string? Link { get; set; }
        public ICollection<RoleMenu> RoleMenus { get; set; }
        public Guid? SubMenuId { get; set; }
    }
}
