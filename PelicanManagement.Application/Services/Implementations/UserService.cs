using AutoMapper;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<(UserAuthResponse Result, UserDto? user)> LoginUser(AuthenticateDto request)
        {
            User user = new User();
            if (UtilityManager.IsMobile(request.Input))
            {
                user = await _userRepository.GetUserByMobile(request.Input);
            }
            else if (UtilityManager.IsValidNationalCode(request.Input))
            {
                user = await _userRepository.GetUserByEmail(request.Input);
            }
            else
            {
                user = await _userRepository.GetUserByUsername(request.Input);
            }

            if (user == null)
            {
                return (UserAuthResponse.NotFound, null);
            }
            if (!user.IsActive)
            {
                return (UserAuthResponse.NotAvtive, null);
            }
            if (user.IsDeleted)
            {
                return (UserAuthResponse.IsDeleted, null);
            }

            var password = _passwordHasher.EncodePasswordMd5(request.Password);
            if (user.Password != password)
            {
                return (UserAuthResponse.WrongPassword, null);
            }

            user.LastLoginDate = DateTime.Now;
            await _userRepository.UpdateAsync(user);
            var mappedUser = _mapper.Map<UserDto>(user);
            return (UserAuthResponse.Success, mappedUser);
        }

        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            return await _userRepository.RegisterUser(requset);
        }
    }
}
