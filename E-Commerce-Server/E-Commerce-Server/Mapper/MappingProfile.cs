using AutoMapper;
using ECom.API.DTO.AuthenticationDTO;
using ECom.API.DTOs.AuthenticationDTO;
using ECom.API.DTOs.ProfileDTOs;
using ECom.BLogic.Services.Models;
using ECom.Data.Account;
namespace ECom.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDTO, UserCredentials>();
            CreateMap<RegisterDTO, UserCredentials>();
            CreateMap<EmailConfirmDTO, EmailConfirmCredentials>();

            CreateMap<UserDTO, EComUser>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.AddressDelivery, opt => opt.MapFrom(src => src.AddressDelivery));
            CreateMap<EComUser, UserDTO>();
            //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
