using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.Pelican;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Enums;
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
        private readonly IIdentityServerRepository _identityServerRepository;
        private readonly ILogService _logService;

        private readonly IPelicanRepository _pelicanRepository;
        private readonly IPelicanGenericRepository<Domain.Entities.Pelican.UserPermission> _permissionGenericRepository;
        private readonly IPelicanGenericRepository<Domain.Entities.Pelican.UsersUnit> _unitGenericRepository;
        private readonly IPasswordHasher<Domain.Entities.IdentityServer.User> _passwordHasher;
        private readonly IMapper _mapper;

        public ManagementService(IIdentityServerRepository identityServerRepository,
            IPasswordHasher<Domain.Entities.IdentityServer.User> passwordHasher,
            IPelicanGenericRepository<Domain.Entities.Pelican.UsersUnit> unitGenericRepository,
            IPelicanGenericRepository<Domain.Entities.Pelican.UserPermission> permissionGenericRepository,
            IMapper mapper, IPelicanRepository pelicanRepository, ILogService logService)
        {
            _identityServerRepository = identityServerRepository;
            _pelicanRepository = pelicanRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _permissionGenericRepository = permissionGenericRepository;
            _unitGenericRepository = unitGenericRepository;
            _logService = logService;
        }

        public async Task<ResponseDto<bool>> AddUser(AddIdentityUserDto request, Guid operatorId, UserType type)
        {
            var isExist = await _identityServerRepository.IsExist(x => x.UserName.Equals(request.UserName) || x.PersonalCode.Equals(request.PersonalCode));
            if (isExist)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.RecordAlreadyExists };
            }
            /// Add To Identity 
            var mappedUser = _mapper.Map<Domain.Entities.IdentityServer.User>(request);
            mappedUser.PasswordHash = _passwordHasher.HashPassword(mappedUser, request.Password);
            await _identityServerRepository.AddAsync(mappedUser);

            if (request.PermissionIds != null)
            {
                foreach (var item in request.PermissionIds)
                {
                    var userPermission = new UserPermission { UserId = mappedUser.UserName, PermissionId = item };
                    await _permissionGenericRepository.AddAsync(userPermission);
                }
            }
            if (request.UnitIds != null)
            {
                foreach (var item in request.UnitIds)
                {
                    var userUnits = new UsersUnit { UserId = mappedUser.Id.ToString(), UnitId = item, CreatedDate = DateTime.Now, Username = mappedUser.UserName };
                    await _unitGenericRepository.AddAsync(userUnits);
                }
            }

            /// Add To Pelican 

            if (type == UserType.Pelican)
            {
                var mappedPelicanUser = _mapper.Map<ApiUser>(request);
                mappedPelicanUser.PasswordHash = mappedUser.PasswordHash;
                try
                {
                    await _pelicanRepository.AddAsync(mappedPelicanUser);

                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            await _logService.InsertUserActivityLog(new UserActivityLogDto
            {
                UserId = operatorId,
                NewValues = mappedUser.UserName,
                UserActivityLogTypeId = (type == UserType.Pelican) ? ActivityLogType.CreatePelicanUser : ActivityLogType.CreateTeriageUser
            });

            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> DeleteUser(int userID, Guid operatorId)
        {
            var user = await _identityServerRepository.Get(userID);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            await _identityServerRepository.Remove(user);
            var userPelcian = await _pelicanRepository.GetByUsername(user.UserName);
            if (userPelcian != null)
            {

                await _pelicanRepository.Remove(userPelcian);

                var units = await _pelicanRepository.GetUserUnits(user.UserName);
                if (units != null)
                {
                    foreach (var item in units)
                    {
                        await _unitGenericRepository.Remove(item);
                    }
                }

                var permisssions = await _pelicanRepository.GetUserPermissions(user.UserName);
                if (permisssions != null)
                {
                    foreach (var item in permisssions)
                    {
                        await _permissionGenericRepository.Remove(item);
                    }
                }
                await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = user.UserName, UserActivityLogTypeId = ActivityLogType.DeletePelicanUser });
            }
            else
            {
                await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = user.UserName, UserActivityLogTypeId = ActivityLogType.DeleteTeriageUser });
            }
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<IEnumerable<Domain.Entities.IdentityServer.User>>> GetPaginatedUsersList(PaginationDto request, UserType type)
        {
            var users = await _identityServerRepository.GetPaginatedUsersList(request, type);
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


        public async Task<ResponseDto<bool>> UpdateUser(int userID, UpdateIdentityUserDto request, Guid operatorId)
        {
            var user = await _identityServerRepository.Get(userID);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            }
            var mappedUser = _mapper.Map(request, user);
            await _identityServerRepository.UpdateAsync(mappedUser);


            var permissions = await _pelicanRepository.GetUserPermissions(user.UserName);
            if (permissions != null)
            {
                await _permissionGenericRepository.RemoveRangeAsync(permissions);

            }
            if (request.PermissionIds != null)
            {
                foreach (var item in request.PermissionIds)
                {
                    var userPermission = new UserPermission { UserId = user.UserName, PermissionId = item };
                    await _permissionGenericRepository.AddAsync(userPermission);
                }
            }
            var units = await _pelicanRepository.GetUserUnits(user.UserName);
            if (units != null)
            {
                await _unitGenericRepository.RemoveRangeAsync(units);
            }
            if (request.UnitIds != null)
            {
                foreach (var item in request.UnitIds)
                {
                    var userUnits = new UsersUnit { UserId = user.Id.ToString(), UnitId = item, CreatedDate = DateTime.Now, Username = user.UserName };
                    await _unitGenericRepository.AddAsync(userUnits);
                }
            }
            if (await _pelicanRepository.IsExist(x => x.UserName.Equals(user.UserName)))
            {
                await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = mappedUser.UserName, UserActivityLogTypeId = ActivityLogType.UpdatePelicanUser });
            }
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = mappedUser.UserName, UserActivityLogTypeId = ActivityLogType.UpdateTeriageUser });
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }


        public async Task<ResponseDto<IdentityUserDetailDto>> GetUserDetailByUserId(string username)
        {
            IdentityUserDetailDto detailDto = new IdentityUserDetailDto();
            var user = await _identityServerRepository.GetByUsername(username);
            if (user == null)
            {
                return new ResponseDto<IdentityUserDetailDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            detailDto.User = user;
            detailDto.UserPermissions = await _pelicanRepository.GetUserPermissionsByUsername(username);
            detailDto.UserUnits = await _pelicanRepository.GetUserUnitsByUsername(username);
            return new ResponseDto<IdentityUserDetailDto> { IsSuccessFull = true, Data = detailDto, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }

        public async Task<ResponseDto<PermissionsAndUnitsDto>> GetPermissionsAndUnits()
        {
            var user = await _pelicanRepository.GetPermissionsAndUnits();
            return new ResponseDto<PermissionsAndUnitsDto> { IsSuccessFull = true, Data = user, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }

        public async Task<ResponseDto<Domain.Entities.IdentityServer.User>> GetTeriageUserDetailByUserId(string username)
        {
            var user = await _identityServerRepository.GetByUsername(username);
            if (user == null)
            {
                return new ResponseDto<Domain.Entities.IdentityServer.User> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            var IsTeriage = await _pelicanRepository.IsExist(x => x.UserName.Equals(username));
            if (IsTeriage)
            {
                return new ResponseDto<Domain.Entities.IdentityServer.User> { IsSuccessFull = false, Message = ErrorsMessages.IsNotTeriageUser };
            }
            return new ResponseDto<Domain.Entities.IdentityServer.User> { IsSuccessFull = true, Data = user, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }
    }
}
