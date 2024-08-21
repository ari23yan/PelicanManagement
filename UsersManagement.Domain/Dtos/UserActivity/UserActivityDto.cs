using UsersManagement.Domain.Entities.UsersManagement.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.UserActivity
{
    public class UserActivityDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public long UserActivityLogTypeId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? Description { get; set; }
    }
}
