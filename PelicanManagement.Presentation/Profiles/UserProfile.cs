using AutoMapper;
using PelicanManagement.Domain.Dtos.Permissions;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;

namespace PelicanManagement.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<Menu, RoleMenusDto>().ReverseMap();
            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<Role, RoleMenusDto>().ReverseMap();
            CreateMap<Role, GetRoleMenuDto>().ReverseMap();

            CreateMap< User, UserDetailDto>()
             .ForMember(dest => dest.RoleName_Farsi, opt => opt.MapFrom(src => src.Role.RoleName_Farsi))
             .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
             .ReverseMap();


            CreateMap<Role, GetRolesListDto>().ReverseMap();

            CreateMap<PermissionsDto, RolePermission>()
                .ReverseMap();
            CreateMap<Permission, PermissionsDto>().ReverseMap();

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                 .ForMember(dest => dest.IsActive, opt => opt.MapFrom((src, dest) => src.IsActive != null ? src.IsActive : dest.IsActive))
                 .ForMember(dest => dest.RoleId, opt => opt.MapFrom((src, dest) => src.RoleId != null ? src.RoleId : dest.RoleId))
                 .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<User, UsersListDto>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName_Farsi));
        }

    }
}
