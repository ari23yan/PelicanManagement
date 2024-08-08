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
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Enums;

namespace PelicanManagement.Application.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRepository<RolePermission> _rolePermisionRepository;
        private readonly IRepository<RoleMenu> _roleMenuRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public RoleService(IRoleRepository roleRepository, IRepository<RoleMenu> roleMenuRepository, IRepository<RolePermission> rolePermissiomRepository, ILogService logService, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logService = logService;
            _rolePermisionRepository = rolePermissiomRepository;
            _roleMenuRepository = roleMenuRepository;
        }
        public async Task<ResponseDto<RoleMenuDto>> GetRoleMenusByRoleId(Guid roleId)
        {
            var role = await _roleRepository.GetRoleWithDetailById(roleId);
            if (role == null)
            {
                return new ResponseDto<RoleMenuDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
            }
            var allPermissions = await _roleRepository.GetAllPermissions();
            var userRolePermissions = role.RolePermissions.Select(x => x.PermissionId).ToList();
            var rolePermissions = allPermissions
           .Select(g => new PermissionsDto
           {
               Id = g.Id,
               PermissionName = g.PermissionName,
               PermissionName_Farsi = g.PermissionName_Farsi,
               Description = g.Description,
               HasPermission = userRolePermissions.Contains(g.Id)
           }).ToList();
            var allMenus = await _roleRepository.GetMenusList();
            var userRolemenuIds = role.RoleMenus.Select(x => x.MenuId).ToList();
            var roleMenu = await GetMenus(userRolemenuIds, allMenus);
            var roleDetailDto = _mapper.Map<RoleMenuDto>(role);
            roleDetailDto.Menus = roleMenu;
            roleDetailDto.Permission = rolePermissions;
            return new ResponseDto<RoleMenuDto> { IsSuccessFull = true, Data = roleDetailDto, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
        }

        public async Task<ResponseDto<RolesListWithPermissionAndMenusDto>> GetRolesPermissionAndMenus(Guid? roleId)
        {
            if (roleId == null)
            {
                var allRoles = await _roleRepository.GetRolesList();
                var roles = allRoles
                 .Select(g => new RoleDto
                 {
                     Id = g.Id,
                     RoleName = g.RoleName,
                     RoleName_Farsi = g.RoleName_Farsi,
                     Description = g.Description,
                     HasRole = false
                 }).ToList();

                var allPermissions = await _roleRepository.GetAllPermissions();
                var rolePermissions = allPermissions
               .Select(g => new PermissionsDto
               {
                   Id = g.Id,
                   PermissionName = g.PermissionName,
                   PermissionName_Farsi = g.PermissionName_Farsi,
                   Description = g.Description,
                   HasPermission = false
               }).ToList();

                var allMenus = await _roleRepository.GetMenusWithoutSubsList();
                var roleMenu = allMenus
               .Select(g => new RoleMenusDto
               {
                   Id = g.Id,
                   Name = g.Name,
                   Name_Farsi = g.Name_Farsi,
                   Description = g.Description,
                   HasMenu = false
               }).ToList();

                var roleDetailDto = new RolesListWithPermissionAndMenusDto();
                roleDetailDto.Role = roles;
                roleDetailDto.Menus = roleMenu;
                roleDetailDto.Permission = rolePermissions;
                return new ResponseDto<RolesListWithPermissionAndMenusDto> { IsSuccessFull = true, Data = roleDetailDto, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
            }
            else
            {
                var role = await _roleRepository.GetRoleWithDetailById(roleId.Value);
                if (role == null)
                {
                    return new ResponseDto<RolesListWithPermissionAndMenusDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound, Status = "NotFound" };
                }


                var allRoles = await _roleRepository.GetRolesList();
                var roles = allRoles
                 .Select(g => new RoleDto
                 {
                     Id = g.Id,
                     RoleName = g.RoleName,
                     RoleName_Farsi = g.RoleName_Farsi,
                     Description = g.Description,
                     HasRole = role.Id == g.Id
                 }).ToList();

                var allPermissions = await _roleRepository.GetAllPermissions();
                var userRolePermissions = role.RolePermissions.Select(x => x.PermissionId).ToList();
                var rolePermissions = allPermissions
               .Select(g => new PermissionsDto
               {
                   Id = g.Id,
                   PermissionName = g.PermissionName,
                   PermissionName_Farsi = g.PermissionName_Farsi,
                   Description = g.Description,
                   HasPermission = userRolePermissions.Contains(g.Id)
               }).ToList();

                var allMenus = await _roleRepository.GetMenusWithoutSubsList();
                var userRolemenuIds = role.RoleMenus.Select(x => x.MenuId).ToList();
                var roleMenu = await GetMenus(userRolemenuIds, allMenus);
                var roleDetailDto = new RolesListWithPermissionAndMenusDto();
                roleDetailDto.Role = roles;
                roleDetailDto.Menus = roleMenu;
                roleDetailDto.Permission = rolePermissions;
                return new ResponseDto<RolesListWithPermissionAndMenusDto> { IsSuccessFull = true, Data = roleDetailDto, Message = ErrorsMessages.OperationSuccessful, Status = "SuccessFull" };
            }
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
            var permissons = await _roleRepository.GetRolePermissions(dto.TargetId.Value);
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
            else if (request.MenuIds == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.MenusIsNotValid };
            }
            else if (request.PermissionIds == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.PermissionIsNotValid };
            }

            request.CreatedBy = operatorId;
            var mappedRole = _mapper.Map<Role>(request);
            await _roleRepository.AddAsync(mappedRole);
            /// --> Role TODO


            List<RoleMenu> newMenuList = new List<RoleMenu>();
            foreach (var item in request.MenuIds)
            {
                var newMenu = new RoleMenu { RoleId = mappedRole.Id, MenuId = item, CreatedBy = operatorId };
                newMenuList.Add(newMenu);
            }
            await _roleMenuRepository.AddRangeAsync(newMenuList);


            /// --> Permission TODO


            List<RolePermission> newPermisisonList = new List<RolePermission>();
            foreach (var item in request.PermissionIds)
            {
                var newMenu = new RolePermission { RoleId = mappedRole.Id, PermissionId = item, CreatedBy = operatorId };
                newPermisisonList.Add(newMenu);
            }
            await _rolePermisionRepository.AddRangeAsync(newPermisisonList);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = request.RoleName_Farsi, UserActivityLogTypeId = ActivityLogType.CreateRole });

            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }

        public async Task<ResponseDto<bool>> UpdateRole(Guid roleId, UpdateRoleDto request, Guid operatorId)
        {
            var role = await _roleRepository.GetRoleWithDetailById(roleId);
            if (role == null)
            {
                return new ResponseDto<bool> { IsSuccessFull = false, Message = ErrorsMessages.NotFound };
            }

            /// --->  Role TODO
            var oldMenus = role.RoleMenus.ToList();

            await _roleMenuRepository.RemoveRangeAsync(oldMenus);

            if (request.MenuIds != null)
            {
                List<RoleMenu> newMenuList = new List<RoleMenu>();
                foreach (var item in request.MenuIds)
                {
                    var newMenu = new RoleMenu { RoleId = role.Id, MenuId = item, CreatedBy = operatorId };
                    newMenuList.Add(newMenu);
                }
                await _roleMenuRepository.AddRangeAsync(newMenuList);
            }

            /// --> Permission TODO

            var oldPermissions = role.RolePermissions.ToList();
            await _rolePermisionRepository.RemoveRangeAsync(oldPermissions);

            if (request.PermissionIds != null)
            {
                List<RolePermission> newPermisisonList = new List<RolePermission>();
                foreach (var item in request.PermissionIds)
                {
                    var newMenu = new RolePermission { RoleId = role.Id, PermissionId = item, CreatedBy = operatorId };
                    newPermisisonList.Add(newMenu);
                }
                await _rolePermisionRepository.AddRangeAsync(newPermisisonList);
            }

            var mappedRole = _mapper.Map(request, role);
            mappedRole.ModifiedBy = operatorId;
            mappedRole.IsModified = true;
            await _roleRepository.UpdateAsync(mappedRole);
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = mappedRole.RoleName_Farsi, UserActivityLogTypeId = ActivityLogType.UpdateUser });
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
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, NewValues = user.RoleName_Farsi, UserActivityLogTypeId = ActivityLogType.DeleteRole });

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
            await _logService.InsertUserActivityLog(new UserActivityLogDto { UserId = operatorId, OldValues = user.RoleName_Farsi, NewValues = user.IsActive == false ? "غیر فعال" : "فعال", UserActivityLogTypeId = ActivityLogType.ActiveOrDeActiveRole });

            return new ResponseDto<bool> { IsSuccessFull = true, Message = ErrorsMessages.OperationSuccessful };
        }
    }
}
