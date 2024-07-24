using Shop.Application.Services.Interfaces;
using Shop.Domain.Dtos.Common.ResponseModel;
using Shop.Domain.Dtos.User;
using Shop.Domain.Enums;
using Shop.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
                _userRepository = userRepository;   
        }
        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            return await _userRepository.RegisterUser(requset); 
        }
    }
}
