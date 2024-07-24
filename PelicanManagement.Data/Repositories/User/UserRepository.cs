using Microsoft.EntityFrameworkCore;
using PelicanManagement.Data.Context;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Application.Security;
using System.Reflection;

namespace PelicanManagement.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
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

        public async Task<User?> GetUserByEmail(string phoneNumber)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(phoneNumber));
        }

        public async Task<User> GetUserById(string id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(mobile));
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }

        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            if (await CheckUserExist(requset.PhoneNumber))
            {
                return new ResponseDto<RegisterUserDto>
                { IsSuccessFull = false, Message = ErrorsMessages.UserExist };
            }
            PelicanManagement.Domain.Entities.Account.User user = new Domain.Entities.Account.User
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
            return new ResponseDto<RegisterUserDto>
            { IsSuccessFull = true, Message = ErrorsMessages.SuccessRegister };
        }
    }
}
