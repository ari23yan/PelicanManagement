using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Role
{
    public class RoleMenusDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Name_Farsi { get; set; }
        public string? Description { get; set; }
        public Guid? SubMenuId { get; set; }
        public bool HasMenu { get; set; } = false;
        public string Link { get; set; }
        public List<RoleMenusDto> SubMenus { get; set; }
    }
}
