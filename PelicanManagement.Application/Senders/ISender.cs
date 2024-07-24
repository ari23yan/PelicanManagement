using Shop.Domain.Dtos.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Senders
{
    public interface ISender
    {
        void SendMailForSignUp(SendMailDto request);
        void SendMailForgetPassword(SendMailDto request);
    }
}
