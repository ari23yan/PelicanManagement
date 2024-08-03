﻿using PelicanManagement.Domain.Dtos.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Senders
{
    public interface ISender
    {
        Task<bool> SendSmsForgetPassword(SendSmsDto sendSmsDto);

    }
}
