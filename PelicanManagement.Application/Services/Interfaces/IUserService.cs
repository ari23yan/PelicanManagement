using Shop.Domain.Dtos.Common.ResponseModel;
using Shop.Domain.Dtos.User;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset);
    }
}
