using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Entities.Common
{
    [Table("UserActivityLog", Schema = "Common")]

    public class UserActivityLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        public long UserActivityLogTypeId { get; set; }
        public UserActivityLogType UserActivityLogType { get; set; }
        public DateTime Timestamp { get; set; }
        [MaxLength(500)]
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        [MaxLength(1024)]
        public string OldValues { get; set; }
        [MaxLength(1024)]
        public string NewValues { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
    }
}
