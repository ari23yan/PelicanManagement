using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Entities.UsersManagement.Common
{
    [Table("UserActivityLogs", Schema = "Common")]

    public class UserActivityLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(100)]
        public long UserActivityLogTypeId { get; set; }
        public UserActivityLogType UserActivityLogType { get; set; }
        public DateTime Timestamp { get; set; }
        [MaxLength(5000)]
        public string? OldValues { get; set; }
        [MaxLength(5000)]
        public string? NewValues { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
    }
}
