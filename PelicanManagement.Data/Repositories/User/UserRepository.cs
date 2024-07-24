using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.Dtos.Common.ResponseModel;
using Shop.Domain.Dtos.User;
using Shop.Domain.Enums;
using Shop.Domain.Interfaces.Product;
using Shop.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities.Account;
using Shop.Application.Security;

namespace Shop.Data.Repositories.User
{
    public class UserRepository : Repository<Shop.Domain.Entities.Account.User>, IUserRepository
    {
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(IPasswordHasher passwordHasher, AppDbContext context) : base(context)
        {
                _passwordHasher = passwordHasher;
        }
        public async Task<bool> CheckUserExist(string phoneNumber)
        {
           return await Context.Users
                .AnyAsync(x=>x.PhoneNumber.Equals(phoneNumber));
        }

        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            if (await CheckUserExist(requset.PhoneNumber))
            {
                return new ResponseDto<RegisterUserDto>
                { IsSuccessFull = false, Message = ErrorsMessages.UserExist };
            }
            Shop.Domain.Entities.Account.User user = new Domain.Entities.Account.User
            {
                PhoneNumber =  requset.PhoneNumber,
                Email = requset.Email,
                Username = requset.PhoneNumber,
                UserRoleId = 2,
                Password =  _passwordHasher.EncodePasswordMd5(requset.Password),
                FirstName = requset.FirstName,
                LastName = requset.LastName,
            };
            await AddAsync(user);
            await SaveAsync();
            return new ResponseDto<RegisterUserDto>
            { IsSuccessFull = true, Message = ErrorsMessages.SuccessRegister };
        }
    }
}
