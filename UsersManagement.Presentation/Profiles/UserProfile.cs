using AutoMapper;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Entities.UsersManagement.Common;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ReverseMap();


            CreateMap<UserActivityLog, UserActivityLogDto>();

            CreateMap<User, AddUserDto>().ReverseMap()
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src =>
             
             src.Password))


                ;
        

            CreateMap< User, UserDetailDto>()
             .ForMember(dest => dest.RoleName_Farsi, opt => opt.MapFrom(src => src.Role.RoleName_Farsi))
             .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
             .ReverseMap();


         

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                 .ForMember(dest => dest.IsActive, opt => opt.Ignore())
                 .ForMember(dest => dest.RoleId, opt => opt.MapFrom((src, dest) => src.RoleId != null ? src.RoleId : dest.RoleId))
                 .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())

                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<User, UsersListDto>()
             .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName_Farsi));
        }

    }
}
