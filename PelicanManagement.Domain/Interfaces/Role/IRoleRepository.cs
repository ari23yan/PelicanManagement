﻿using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces
{
    public interface IRoleRepository: IRepository<Role>
    {
        Task<IEnumerable<Role>> GetRolesList();
        Task<Role> GetRoleById(Guid roleId);
        Task<IEnumerable<Menu>> GetRoleMenusByRoleId(Guid roleId);
        Task<Role> GetActiveORDeActiveRoleById(Guid roleId);
        Task<Role> GetRoleWithDetailById(Guid roleId);
        Task<IEnumerable<Permission>> GetAllPermissions();
        Task<IEnumerable<Permission>> GetRolePermissions(Guid roleId);
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<IEnumerable<Menu>> GetMenusList();
        Task<IEnumerable<Menu>> GetMenusWithoutSubsList();
        Task<ListResponseDto<Role>> GetPaginatedRolesList(PaginationDto request);
    }
}
