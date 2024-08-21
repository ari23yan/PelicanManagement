using UsersManagement.Domain.Dtos.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Senders
{
    public interface ISender
    {
        Task<bool> SendSmsForgetPassword(SendSmsDto sendSmsDto);

    }
}
