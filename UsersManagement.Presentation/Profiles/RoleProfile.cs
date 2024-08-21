using AutoMapper;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Entities.UsersManagement.Account;

namespace UsersManagement.Presentation.Profiles
{
    public class RoleProfile : Profile
    {

        public RoleProfile()
        {
            CreateMap<Menu, RoleMenusDto>().ReverseMap();

            CreateMap<Role, RoleMenusDto>().ReverseMap();
            CreateMap<Role, RoleMenuDto>().ReverseMap();

            CreateMap<Role, RolesListDto>().ReverseMap();

            CreateMap<PermissionsDto, RolePermission>()
                .ReverseMap();
            CreateMap<Permission, PermissionsDto>().ReverseMap();


            CreateMap<Role, AddRoleDto>().ReverseMap();

            CreateMap<Role, UpdateRoleDto>()
            .ForMember(dest => dest.PermissionIds, opt => opt.Ignore())
            .ForMember(dest => dest.MenuIds, opt => opt.Ignore())
                .ReverseMap();





        }
    }
}
