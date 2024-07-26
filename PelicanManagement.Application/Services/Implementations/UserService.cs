using AutoMapper;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Senders;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common.AccessLog;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Permissions;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.Sender;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly ISender _sender;
        private readonly ILogService _logService;

        public UserService(IUserRepository userRepository, ILogService logService , ISender sender, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _sender = sender;
            _logService = logService;
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


            if (!user.EmailConfirmed)
            {
                var otp = UtilityManager.OtpGenrator();
                await _sender.ConfirmEmailAddress(new SendMailDto
                {
                    Code = otp,
                    Email = user.Email,
                    Username = user.Username,
                });
                user.OtpCode = otp;
                await _userRepository.UpdateAsync(user);
                return (UserAuthResponse.EmailNotConfirmed, new UserDto { Email = user.Email});
            }
            user.LastLoginDate = DateTime.Now;
            await _userRepository.UpdateAsync(user);
            var mappedUser = _mapper.Map<UserDto>(user);
            //var menu = await GetRoleMenusByRoleId(user.RoleId);
            //mappedUser.RoleMenus = menu.Data;
            return (UserAuthResponse.Success, mappedUser);
        }

        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            return await _userRepository.RegisterUser(requset);
        }

        public async Task<ResponseDto<GetRoleMenuDto>> GetRoleMenusByRoleId(Guid roleId)
        {
            var role = await _userRepository.GetRoleWithDetailById(roleId);
            if (role == null)
            {
                return new ResponseDto<GetRoleMenuDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
            }
            var allPermissions = await _userRepository.GetAllPermissions();
            var allMenus = await _userRepository.GetMenusList();
            var userRolemenuIds = role.RoleMenus.Select(x => x.MenuId).ToList();
            var roleMenu = await GetMenus(userRolemenuIds, allMenus);
            var roleDetailDto = _mapper.Map<GetRoleMenuDto>(role);
            roleDetailDto.Menus = roleMenu;
            return new ResponseDto<GetRoleMenuDto> { IsSuccessFull = true, Data = roleDetailDto, Message = ErrorsMessages.Success, Status = "SuccessFull" };
        }


        public async Task<ResponseDto<IEnumerable<UsersListDto>>> GetPaginatedUsersList(PaginationDto request)
        {
            var users = await _userRepository.GetPaginatedUsersList(request);
            var mappedUsers = _mapper.Map<IEnumerable<User>, IEnumerable<UsersListDto>>(users.List);
            return new ResponseDto<IEnumerable<UsersListDto>>
            {
                IsSuccessFull = true,
                Data = mappedUsers,
                Message = ErrorsMessages.Success,
                Status = "SuccessFul",
                TotalCount = users.TotalCount
            };
        }

        public async Task<ResponseDto<AddUserDto>> AddUser(AddUserDto request, Guid operatorId)
        {
            if (await _userRepository.CheckUserExistByPhoneMumber(request.PhoneNumber))
            {
                return new ResponseDto<AddUserDto> { IsSuccessFull = false, Message = ErrorsMessages.PhoneNumberAlreadyExist };
            }
            if (await _userRepository.CheckUserExistByUsername(request.Username))
            {
                return new ResponseDto<AddUserDto> { IsSuccessFull = false, Message = ErrorsMessages.UsernameAlreadyExist };
            }
            if (await _userRepository.CheckUserExistByEmail(request.Email))
            {
                return new ResponseDto<AddUserDto> { IsSuccessFull = false, Message = ErrorsMessages.UsernameAlreadyExist };
            }
            request.CreatedBy = operatorId;
            var mappedUser = _mapper.Map<User>(request);
            await _userRepository.AddAsync(mappedUser);
            return new ResponseDto<AddUserDto> { IsSuccessFull = true, Message = ErrorsMessages.Success};
        }
        public async Task<ResponseDto<bool>> DeleteUserByUserId(Guid userId, Guid operatorId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.Faild };
            }
            user.IsDeleted = true;
            user.DeletedDate = DateTime.UtcNow;
            user.DeletedBy = operatorId;
            await _userRepository.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.Success };
        }

        public async Task<ResponseDto<bool>> UpdateUserByUserId(Guid userId, UpdateUserDto request, Guid operatorId)
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
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.PhoneNumberAlreadyExist};
                }
            }
            if (request.Username != null && request.Username != "" && user.Username != request.Username)
            {
                if (await _userRepository.CheckUserExistByUsername(request.Username))
                {
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.UsernameAlreadyExist };
                }
            }

            if (request.Email != null && request.Email != "" && user.Email != request.Email)
            {
                if (await _userRepository.CheckUserExistByEmail(request.Email))
                {
                    return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.EmailAlreadyExist };
                }
            }

         
            var mappedUser = _mapper.Map(request, user);
            mappedUser.ModifiedBy = operatorId;
            mappedUser.IsModified = true;
            await _userRepository.UpdateAsync(mappedUser);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.Success };

        }


        public async Task<ResponseDto<UserDetailDto>> GetUserDetailByUserId(Guid userId)
        {
            var user = await _userRepository.GetUserDetailById(userId);
            if (user == null)
            {
                return new ResponseDto<UserDetailDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }
            
            var userDetailDto = _mapper.Map<UserDetailDto>(user);

            var allMenus = await _userRepository.GetMenusList();
            var userRolemenuIds = user.Role.RoleMenus.Select(x => x.MenuId).ToList();
            var roleMenu = await GetMenus(userRolemenuIds, allMenus);

            userDetailDto.Menus = roleMenu;

            var permission = user.Role.RolePermissions.Select(x => x.Permission);

            var permissionsDto = _mapper.Map<List<PermissionsDto>>(permission);
            userDetailDto.Permissions = permissionsDto;
            return new ResponseDto<UserDetailDto> { IsSuccessFull = true, Data = userDetailDto, Message = ErrorsMessages.Success};
        }

        public async Task<ResponseDto<IEnumerable<GetRolesListDto>>> GetRolesList()
        {
            var roleList = await _userRepository.GetRolesList();
            var mappedRoleList = _mapper.Map<IEnumerable<GetRolesListDto>>(roleList);
            return new ResponseDto<IEnumerable<GetRolesListDto>> { IsSuccessFull = true, Data = mappedRoleList, Message = ErrorsMessages.Success};
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
    }
}
