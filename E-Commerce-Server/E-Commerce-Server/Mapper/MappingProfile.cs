using AutoMapper;
using ECom.API.DTO.AuthenticationDTO;
using ECom.API.DTOs.AuthenticationDTO;
using ECom.API.DTOs.ProductDTOs;
using ECom.API.DTOs.ProfileDTOs;
using ECom.BLogic.Services.Models;
using ECom.BLogic.Templates;
using ECom.Constants;
using ECom.Data.Account;
using ECom.Data.Models;
using ECom.Extensions;
using Microsoft.IdentityModel.Tokens;
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

            CreateMap<ProductsSearchDTO, SearchQuery<Product>>()
                .ConstructUsing(src => new SearchQuery<Product>
                {
                    Expression = x =>
                        (string.IsNullOrEmpty(src.Name) || x.Name.Contains(src.Name)) &&
                        (src.Platform.IsNullOrEmpty() || x.Platform == Enum.Parse<DataEnums.Platform>(src.Platform!)) &&
                        (!src.DateCreated.HasValue || x.DateCreated == src.DateCreated) &&
                        (!src.TotalRating.HasValue || x.TotalRating == src.TotalRating) &&
                        (!src.Price.HasValue || x.Price == src.Price)
                });
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform.GetPlatformString()));
        }
    }
}
