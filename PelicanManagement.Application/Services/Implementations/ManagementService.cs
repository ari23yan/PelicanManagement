using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.Pelican;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Interfaces;
using PelicanManagement.Domain.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Implementations
{
    public class ManagementService : IManagementService
    {
        private readonly IIdentityServerRepository _identityServer;
        private readonly IPelicanRepository _pelicanRepository;
        private readonly IPasswordHasher<Domain.Entities.IdentityServer.User> _passwordHasher;


        private readonly IMapper _mapper;

        public ManagementService(IIdentityServerRepository identityServerRepository,IPasswordHasher<Domain.Entities.IdentityServer.User>  passwordHasher, IMapper mapper, IPelicanRepository pelicanRepository)
        {
            _identityServer = identityServerRepository;
            _pelicanRepository = pelicanRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseDto<bool>> AddUser(AddIdentityUserDto request, Guid operatorId)
        {
            var isExist = await _identityServer.IsExist(x => x.UserName.Equals(request.UserName) || x.PersonalCode.Equals(request.PersonalCode));
            if (isExist)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.RecordAlreadyExists };
            }
            var mappedUser = _mapper.Map<Domain.Entities.IdentityServer.User>(request);
            mappedUser.PasswordHash = _passwordHasher.HashPassword(mappedUser, request.Password);
            await _identityServer.AddAsync(mappedUser);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> DeleteUser(int userID, Guid operatorId)
        {
            var user = await _identityServer.Get(userID);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            _identityServer.Remove(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<IEnumerable<Domain.Entities.IdentityServer.User>>> GetPaginatedUsersList(PaginationDto request)
        {
            var users = await _identityServer.GetPaginatedUsersList(request);
            return new ResponseDto<IEnumerable<Domain.Entities.IdentityServer.User>>
            {
                IsSuccessFull = true,
                Data = users.List,
                Message = ErrorsMessages.OperationSuccessful,
                Status = "SuccessFul",
                TotalCount = users.TotalCount
            };
        }
        public async Task<ResponseDto<IEnumerable<ApiUser>>> GetPelicanPaginatedUsersList(PaginationDto request)
        {
            var users = await _pelicanRepository.GetPaginatedUsersList(request);
            return new ResponseDto<IEnumerable<ApiUser>>
            {
                IsSuccessFull = true,
                Data = users.List,
                Message = ErrorsMessages.OperationSuccessful,
                Status = "SuccessFul",
                TotalCount = users.TotalCount
            };
        }

        public async Task<ResponseDto<Domain.Entities.IdentityServer.User>> GetUserByUserId(int userId)
        {
            var user = await _identityServer.Get(userId);
            if (user == null)
            {
                return new ResponseDto<Domain.Entities.IdentityServer.User> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
            }
            return new ResponseDto<Domain.Entities.IdentityServer.User> { IsSuccessFull = true, Data = user, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }

        public async Task<ResponseDto<ApiUser>> GetUserByUserId(string userId)
        {
            var user = await _pelicanRepository.Get(userId);
            if (user == null)
            {
                return new ResponseDto<ApiUser> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
            }
            return new ResponseDto<ApiUser> { IsSuccessFull = true, Data = user, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };

        }

        public async Task<ResponseDto<bool>> UpdateUser(int userID, UpdateIdentityUserDto request, Guid operatorId)
        {
            var user = await _identityServer.Get(userID);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            user.PersonalCode = request.PersoanlCode;
            await _identityServer.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }
    }
}
