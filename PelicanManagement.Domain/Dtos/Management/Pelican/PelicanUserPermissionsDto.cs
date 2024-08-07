using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.Pelican
{
    public class PelicanUserPermissionsDto
    {
        public long Id { get; set; }
        public long PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public bool HasPermission { get; set; }
    }
}
