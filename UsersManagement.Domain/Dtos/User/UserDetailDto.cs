﻿using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.User
{
    public class UserDetailDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? IsCreatedBy { get; set; }
        public Guid RoleId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string RoleName { get; set; }
        public string RoleName_Farsi { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }
        public bool IsModified { get; set; } = false;
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public List<PermissionsDto> Permissions { get; set; }
        public List<RoleMenusDto> Menus { get; set; }
        public List<RolesListDto> AllRoles { get; set; }
        public List<UserActivityLogDto> UserActivities { get; set; }
    }
}
