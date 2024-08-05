using AutoMapper;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Entities.IdentityServer;

namespace PelicanManagement.Presentation.Profiles
{
    public class ManagentProfile : Profile
    {
        public ManagentProfile()
        {
            CreateMap<User, AddIdentityUserDto>()
            .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => UtilityManager.GenerateSecurityStamp()))
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => Guid.NewGuid() ))
            .ReverseMap();
        }
    }
}
