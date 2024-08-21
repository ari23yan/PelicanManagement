using AutoMapper;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Entities.IdentityServer;
using UsersManagement.Domain.Entities.Pelican;

namespace UsersManagement.Presentation.Profiles
{
    public class ManagementProfile : Profile
    {
        public ManagementProfile()
        {
            CreateMap<AddIdentityUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => UtilityManager.GenerateSecurityStamp()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ReverseMap()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
                .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore());




            CreateMap<AddIdentityUserDto, ApiUser>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + "" + src.LastName))
           .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => UtilityManager.GenerateSecurityStamp()))
           .ReverseMap()
           .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
           .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
           .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
           .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
           .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
           .ForMember(dest => dest.Email, opt => opt.Ignore())
           .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
           .ForMember(dest => dest.Password, opt => opt.Ignore());


            CreateMap<User, UpdateIdentityUserDto>()
               .ForMember(dest => dest.PermissionIds, opt => opt.Ignore())
               .ForMember(dest => dest.UnitIds, opt => opt.Ignore())
               .ForMember(dest => dest.Password, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
               .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
               .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
               .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
               .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
               .ForMember(dest => dest.Email, opt => opt.Ignore())
               .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
               .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
               .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
               .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
               .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
                ;


        }
    }
}
