﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Dtos.Common
{
    public class GetByIdDto
    {
        public Guid? TargetId { get; set; }
    }
}
