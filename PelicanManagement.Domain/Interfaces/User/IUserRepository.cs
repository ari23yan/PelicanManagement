using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckUserExist(string phoneNumber);
        Task<User> GetUserByMobile(string mobile);
        Task<User?> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserById(string id);
        Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset);
    }
}
