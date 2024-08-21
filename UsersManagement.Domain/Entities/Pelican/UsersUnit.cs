using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.Pelican
{
    public class UsersUnit
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public int UnitId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
