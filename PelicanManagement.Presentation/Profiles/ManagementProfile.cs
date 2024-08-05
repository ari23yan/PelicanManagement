using AutoMapper;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Entities.IdentityServer;

namespace PelicanManagement.Presentation.Profiles
{
    public class ManagementProfile : Profile
    {
        public ManagementProfile()
        {
            CreateMap<AddIdentityUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => UtilityManager.GenerateSecurityStamp()))
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}
