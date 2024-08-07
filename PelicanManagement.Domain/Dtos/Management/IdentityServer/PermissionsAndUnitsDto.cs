using PelicanManagement.Domain.Dtos.Management.Pelican;
using PelicanManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Management.IdentityServer
{
    public class PermissionsAndUnitsDto
    {
        public List<Units> UserUnits { get; set; }
        public List<Permission> UserPermissions { get; set; }
    }
}
