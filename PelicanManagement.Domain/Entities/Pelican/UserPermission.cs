using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Entities.Pelican
{
    public class UserPermission
    {
        public long Id { get; set; }
        public long PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public string UserId { get; set; }
        public string? Data { get; set; }
    }
}
