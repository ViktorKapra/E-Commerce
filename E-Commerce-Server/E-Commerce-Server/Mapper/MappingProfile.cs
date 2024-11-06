using AutoMapper;
using ECom.API.DTO.AuthenticationDTO;
using ECom.API.DTOs.AuthenticationDTO;
using ECom.BLogic.Services.Models;
namespace ECom.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDTO, UserCredentials>();
            CreateMap<RegisterDTO, UserCredentials>();
            CreateMap<EmailConfirmDTO, EmailConfirmCredentials>();
        }
    }
}
