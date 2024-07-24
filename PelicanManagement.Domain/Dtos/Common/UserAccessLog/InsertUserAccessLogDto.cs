﻿using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Common.AccessLog
{
    public class InsertUserAccessLogDto
    {
        public Guid UserId { get; set; }
        public Guid OperatorId { get; set; }

    }
}
