using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Role
{
    public class MenuNode
    {
        public RoleMenusDto Menu { get; set; }
        public List<MenuNode> SubMenus { get; set; } = new List<MenuNode>();
    }
}
