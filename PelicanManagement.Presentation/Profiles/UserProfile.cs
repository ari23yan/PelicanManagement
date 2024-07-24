using AutoMapper;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;

namespace PelicanManagement.Presentation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
