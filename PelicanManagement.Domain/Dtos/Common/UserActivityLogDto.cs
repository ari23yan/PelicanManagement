using PelicanManagement.Domain.Entities.PelicanManagement.Common;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Common
{
    public class UserActivityLogDto
    {
        public Guid UserId { get; set; }
        [MaxLength(100)]
        public ActivityLogType UserActivityLogTypeId { get; set; }
        public DateTime Timestamp { get; set; }
        [MaxLength(5000)]
        public string OldValues { get; set; }
        [MaxLength(5000)]
        public string NewValues { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }
    }
}
