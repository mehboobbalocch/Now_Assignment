using Application.Features.User.Commands;
using AutoMapper;
using Domain.Entites;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SignupUserCommand, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email.ToUpperInvariant()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.BrowserName, opt => opt.MapFrom(src => src.Browser))
                .ForMember(dest => dest.IPAddress, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Balance, opt => opt.Ignore()); 
        }
    }
}
