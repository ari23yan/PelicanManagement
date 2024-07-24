using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset);
        Task<(UserAuthResponse Result, UserDto? user)> LoginUser(AuthenticateDto input);
    }
}
