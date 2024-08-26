using AutoMapper;
using UsersManagement.Application.Security;
using UsersManagement.Application.Senders;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Account;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.AccessLog;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.Sender;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        private readonly ILogService _logService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, ILogService logService, ISender sender, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _sender = sender;
            _logService = logService;
            _roleRepository = roleRepository;
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
        public async Task<bool> CheckUserHavePermission(Guid roleId, Guid permissionId)
        {
            return await _userRepository.CheckUserHavePermission(roleId, permissionId);
        }

        public async Task<ResponseDto<IEnumerable<UsersListDto>>> GetPaginatedUsersList(PaginationDto request)
        {
            var users = await _userRepository.GetPaginatedUsersList(request);
            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UsersListDto>>(users.List);
            return new ResponseDto<IEnumerable<UsersListDto>>
            {
                IsSuccessFull = true,
                Data = mappedUsers,
                Message = ErrorsMessages.OperationSuccessful,
                Status = "SuccessFul",
                TotalCount = users.TotalCount
            };
        }

        public async Task<ResponseDto<bool>> AddUser(AddUserDto request, Guid operatorId)
        {
            if (await _userRepository.CheckUserExistByPhoneMumber(request.PhoneNumber))
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.PhoneNumberAlreadyExists };
            }
            if (await _userRepository.CheckUserExistByUsername(request.Username))
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.UsernameAlreadyExists };
            }
            if (await _userRepository.CheckUserExistByEmail(request.Email))
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.EmailAlreadyExists };
            }
            request.CreatedBy = operatorId;
            request.Password = _passwordHasher.EncodePasswordMd5(request.Password);
            var mappedUser = _mapper.Map<User>(request);
            await _userRepository.AddAsync(mappedUser);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId,NewValues = request.Username, UserActivityLogTypeId = ActivityLogType.CreateUser });
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }
        public async Task<ResponseDto<bool>> DeleteUserByUserId(Guid userId, Guid operatorId)
        {
            var user = await _userRepository.GetActiveORDeActiveUserById(userId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.OperationFailed };
            }
            user.IsDeleted = true;
            user.DeletedDate = DateTime.UtcNow;
            user.DeletedBy = operatorId;
            await _userRepository.UpdateAsync(user);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId,NewValues = user.Username, UserActivityLogTypeId = ActivityLogType.DeleteUser });

            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };

        }
        public async Task<ResponseDto<bool>> ToggleActiveStatusByUserId(Guid userId, Guid operatorId)
        {
            var user = await _userRepository.GetActiveORDeActiveUserById(userId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.OperationFailed };
            }

            user.IsActive = user.IsActive == false ? true : false;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = operatorId;
            await _userRepository.UpdateAsync(user);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId,OldValues=user.Username,NewValues= user.IsActive == false ? "غیر فعال" : "فعال" , UserActivityLogTypeId = ActivityLogType.ActiveOrDeActiveUser });

            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> UpdateUser(Guid userId, UpdateUserDto request, Guid operatorId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            if (request.PhoneNumber != null && request.PhoneNumber != "" && user.PhoneNumber != request.PhoneNumber)
            {
                if (await _userRepository.CheckUserExistByPhoneMumber(request.PhoneNumber))
                {
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.PhoneNumberAlreadyExists };
                }
            }
            if (request.Username != null && request.Username != "" && user.Username != request.Username)
            {
                if (await _userRepository.CheckUserExistByUsername(request.Username))
                {
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.UsernameAlreadyExists };
                }
            }

            if (request.Email != null && request.Email != "" && user.Email != request.Email)
            {
                if (await _userRepository.CheckUserExistByEmail(request.Email))
                {
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.EmailAlreadyExists };
                }
            }


            var mappedUser = _mapper.Map(request, user);
            mappedUser.ModifiedBy = operatorId;
            mappedUser.IsModified = true;
            await _userRepository.UpdateAsync(mappedUser);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId,NewValues = mappedUser.Username, UserActivityLogTypeId = ActivityLogType.UpdateUser });
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }


        public async Task<ResponseDto<UserDetailDto>> GetUserDetailByUserId(Guid userId)
        {
            var user = await _userRepository.GetUserDetailById(userId);
            if (user == null)
            {
                return new ResponseDto<UserDetailDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }

            var userActivitiesDto = await _userRepository.GetActivitiesLogByUserId(userId);
            var userDetailDto = _mapper.Map<UserDetailDto>(user);

            var allMenus = await _roleRepository.GetMenusList();
            var allPermissions = await _roleRepository.GetAllPermissions();
            var userRolemenuIds = user.Role.RoleMenus.Select(x => x.MenuId).ToList();
            var roleMenu = await GetMenus(userRolemenuIds, allMenus);
            var allRoles = await GetRolesList();
            var userRolePermissions = user.Role.RolePermissions.Select(x => x.PermissionId).ToList();
            userDetailDto.Menus = roleMenu;
            userDetailDto.AllRoles = allRoles.Data.ToList();
            userDetailDto.UserActivities = _mapper.Map<List<UserActivityLogDto>>(userActivitiesDto);

            var allPermission = allPermissions.OrderBy(x => x.CreatedDate)
               .Select(g => new PermissionsDto
               {
                   Id = g.Id,
                   PermissionName = g.PermissionName,
                   PermissionName_Farsi = g.PermissionName_Farsi,
                   Description = g.Description,
                   HasPermission = userRolePermissions.Contains(g.Id)
               }).ToList();
            userDetailDto.Permissions = allPermission;
            return new ResponseDto<UserDetailDto> { IsSuccessFull = true, Data = userDetailDto, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<List<RoleMenusDto>> GetMenus(List<Guid> menuIds, IEnumerable<Menu> allMenus)
        {
            var mappedAllMenus = _mapper.Map<List<RoleMenusDto>>(allMenus);
            var res = await BuildMenuHierarchy(mappedAllMenus);
            foreach (var item in res)
            {
                if (item.SubMenus != null && item.SubMenus.Count() > 0)
                {
                    foreach (var subItem in item.SubMenus)
                    {
                        if (menuIds.Contains(subItem.Id))
                        {
                            subItem.HasMenu = true;
                        }
                    }
                }
                if (menuIds.Contains(item.Id))
                {
                    item.HasMenu = true;
                }

            }
            return res;
        }
        public async Task<List<RoleMenusDto>> BuildMenuHierarchy(IEnumerable<RoleMenusDto> allMenus)
        {
            var menuDict = allMenus.ToDictionary(menu => menu.Id, menu => menu);
            var rootMenus = new List<RoleMenusDto>();

            foreach (var menu in allMenus)
            {
                if (menuDict.TryGetValue(menu.Id, out var menuNode))
                {
                    if (menu.SubMenuId.HasValue && menuDict.TryGetValue(menu.SubMenuId.Value, out var parentMenuNode))
                    {
                        if (parentMenuNode.SubMenus == null)
                        {
                            parentMenuNode.SubMenus = new List<RoleMenusDto>();
                        }
                        parentMenuNode.SubMenus.Add(menuNode);
                    }
                    else
                    {
                        rootMenus.Add(menuNode);
                    }
                }
            }
            return rootMenus;
        }
        public async Task<ResponseDto<IEnumerable<RolesListDto>>> GetRolesList()
        {
            var roleList = await _roleRepository.GetRolesList();
            var mappedRoleList = _mapper.Map<IEnumerable<RolesListDto>>(roleList);
            return new ResponseDto<IEnumerable<RolesListDto>> { IsSuccessFull = true, Data = mappedRoleList, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> ChangePassword(Guid userId, string password)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            user.Password = _passwordHasher.EncodePasswordMd5(password);
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = userId;
            await _userRepository.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> ForgetPassword(string mobile)
        {
            var user = await _userRepository.GetUserByMobile(mobile);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            var otp = UtilityManager.OtpGenrator();
            user.OtpCode = otp;
            await _userRepository.UpdateAsync(user);
            await _sender.SendSmsForgetPassword(new SendSmsDto { Code = otp, Username = user.Username, PhoneNumber = user.PhoneNumber });
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OtpSent };
        }

        public async Task<ResponseDto<bool>> ConfirmOtp(ConfrimOtpDto request)
        {
            var user = await _userRepository.GetUserByMobile(request.PhoneNumber);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            if(!user.OtpCode.Equals(request.Otp))
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.IncorrectOtp };
            }
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> SubmitPassword(ForgetPasswordDto request)
        {
            var user = await _userRepository.GetUserByMobile(request.PhoneNumber);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            user.Password = _passwordHasher.EncodePasswordMd5(request.Password);
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = user.Id;
            await _userRepository.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }
    }
}
