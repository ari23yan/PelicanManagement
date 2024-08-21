using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Farapayamak;
using Microsoft.Extensions.Configuration;
using UsersManagement.Domain.Dtos.Sender;
using static System.Net.WebRequestMethods;


namespace UsersManagement.Application.Senders
{
    public class Sender : ISender
    {
        public static string Username = "miladhospital";
        public static string Password = "S1394#Desk";

        public async Task<bool> SendSmsForgetPassword(SendSmsDto request)
        {
            try
            {
                RestClient restClient = new RestClient(Username, Password);
                restClient.BaseServiceNumber(request.Code, request.PhoneNumber, 237917);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
