using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Senders;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Permissions;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public RoleService(IRoleRepository roleRepository, ILogService logService, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logService = logService;
        }
        public async Task<ResponseDto<RoleMenuDto>> GetRoleMenusByRoleId(Guid roleId)
        {
            var role = await _roleRepository.GetRoleWithDetailById(roleId);
            if (role == null)
            {
                return new ResponseDto<RoleMenuDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
            }
            var allPermissions = await _roleRepository.GetAllPermissions();
            var allMenus = await _roleRepository.GetMenusList();
            var userRolemenuIds = role.RoleMenus.Select(x => x.MenuId).ToList();
            var roleMenu = await GetMenus(userRolemenuIds, allMenus);
            var roleDetailDto = _mapper.Map<RoleMenuDto>(role);
            roleDetailDto.Menus = roleMenu;
            return new ResponseDto<RoleMenuDto> { IsSuccessFull = true, Data = roleDetailDto, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }
        public async Task<ResponseDto<IEnumerable<RolesListDto>>> GetRolesList()
        {
            var roleList = await _roleRepository.GetRolesList();
            var mappedRoleList = _mapper.Map<IEnumerable<RolesListDto>>(roleList);
            return new ResponseDto<IEnumerable<RolesListDto>> { IsSuccessFull = true, Data = mappedRoleList, Message = ErrorsMessages.OperationSuccessful };
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
        public async Task<ResponseDto<IEnumerable<PermissionsDto>>> GetRolePermissionsByRoleId(GetByIdDto dto)
        {
            var permissons = await _roleRepository.GetRolePermissions(dto.TargetId);
            var allPermissions = await _roleRepository.GetAllPermissions();
            var userRolePermissions = permissons.Select(x => x.Id).ToList();
            var rolePermissions = allPermissions
           .Select(g => new PermissionsDto
           {
               Id = g.Id,
               PermissionName = g.PermissionName,
               PermissionName_Farsi = g.PermissionName_Farsi,
               Description = g.Description,
               HasPermission = userRolePermissions.Contains(g.Id)
           }).ToList();
            return new ResponseDto<IEnumerable<PermissionsDto>> { IsSuccessFull = true, Data = rolePermissions, Message = ErrorsMessages.OperationSuccessful };

        }
        public async Task<ResponseDto<IEnumerable<RolesListDto>>> GetPaginatedRolesList(PaginationDto request)
        {
            var roles = await _roleRepository.GetPaginatedRolesList(request);
            var mappedRoles = _mapper.Map<IEnumerable<Role>, IEnumerable<RolesListDto>>(roles.List);
            return new ResponseDto<IEnumerable<RolesListDto>>
            {
                IsSuccessFull = true,
                Data = mappedRoles,
                Message = ErrorsMessages.OperationSuccessful,
                Status = "SuccessFul",
                TotalCount = roles.TotalCount
            };
        }

        public async Task<ResponseDto<bool>> AddRole(AddRoleDto request, Guid operatorId)
        {
            if (await _roleRepository.IsExist(x => x.RoleName.Equals(request.RoleName)))
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.RecordAlreadyExists };
            }
            request.CreatedBy = operatorId;
            var mappedRole = _mapper.Map<Role>(request);
            await _roleRepository.AddAsync(mappedRole);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> UpdateRole(Guid roleId, UpdateRoleDto request, Guid operatorId)
        {
            var role = await _roleRepository.GetRoleById(roleId);
            if (role == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }

            var mappedRole = _mapper.Map(request, role);
            mappedRole.ModifiedBy = operatorId;
            mappedRole.IsModified = true;
            await _roleRepository.UpdateAsync(mappedRole);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> DeleteRoleByRoleId(Guid roleId, Guid operatorId)
        {
            var user = await _roleRepository.GetActiveORDeActiveRoleById(roleId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.OperationFailed };
            }
            user.IsDeleted = true;
            user.DeletedDate = DateTime.UtcNow;
            user.DeletedBy = operatorId;
            await _roleRepository.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> ToggleActiveStatusByRoleId(Guid roleId, Guid operatorId)
        {
            var user = await _roleRepository.GetActiveORDeActiveRoleById(roleId);
            if (user == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.OperationFailed };
            }

            user.IsActive = user.IsActive == false ? true : false;
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = operatorId;
            await _roleRepository.UpdateAsync(user);
            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }
    }
}
