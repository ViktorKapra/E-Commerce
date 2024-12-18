﻿using AutoMapper;
using ECom.API.Exchanges.Authentication;
using ECom.API.Exchanges.Order;
using ECom.API.Exchanges.Product;
using ECom.API.Exchanges.Profile;
using ECom.BLogic.DTOs;
using ECom.BLogic.Services.DTOs;
using ECom.BLogic.Templates;
using ECom.Constants;
using ECom.Data.Account;
using ECom.Data.Models;
using Microsoft.IdentityModel.Tokens;
namespace ECom.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginRequest, UserCredentialsDTO>();

            CreateMap<RegisterRequest, UserCredentialsDTO>();

            CreateMap<EmailConfirmRequest, EmailConfirmDTO>();

            CreateMap<UserExchange, UserDTO>().ReverseMap();

            CreateMap<UserDTO, EComUser>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.AddressDelivery, opt => opt.MapFrom(src => src.AddressDelivery));

            CreateMap<EComUser, UserDTO>();

            CreateMap<ProductsSearchRequest, ProductSearchDTO>().ReverseMap();

            CreateMap<ProductSearchDTO, Query<Product>>()
                .ConstructUsing(src => new Query<Product>
                {
                    Expression = x =>
                        (string.IsNullOrEmpty(src.Name) || x.Name.Contains(src.Name)) &&
                        (src.Platform.IsNullOrEmpty() || x.Platform == Enum.Parse<DataEnums.Platform>(src.Platform!)) &&
                        (!src.DateCreated.HasValue || x.DateCreated == src.DateCreated) &&
                        (!src.TotalRating.HasValue || x.TotalRating == src.TotalRating) &&
                        (!src.Price.HasValue || x.Price == src.Price) &&
                        (string.IsNullOrEmpty(src.Genre) || x.Genre == src.Genre) &&
                        (string.IsNullOrEmpty(src.Rating) || x.Rating == Enum.Parse<DataEnums.Rating>(src.Rating!))
                });

            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => Enum.Parse<DataEnums.Platform>(src.Platform!)))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => Enum.Parse<DataEnums.Rating>(src.Rating!)));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => Enum.GetName(src.Platform)))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => Enum.GetName(src.Rating)));

            CreateMap<ProductRequest, ProductDTO>();

            CreateMap<ProductDTO, ProductResponse>();

            CreateMap<ProductRequest, ProductImagesDTO>()
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background));

            CreateMap<ProductRatingExchange, ProductRatingDTO>().ReverseMap();

            CreateMap<ProductRatingDTO, ProductRating>().ReverseMap();

            CreateMap<ProductFilterRequest, ProductFilterDTO>()
                .ForMember(dest => dest.OrderType, opt => opt.Condition(src => (src.OrderType is not null)))
                .ForMember(dest => dest.OrderPropertyName, opt => opt.Condition(src => (src.OrderPropertyName is not null)));

            CreateMap<ProductFilterDTO, FilterQuery<Product>>()
               .ConstructUsing(src => new FilterQuery<Product>
               {
                   Expression = x => ((string.IsNullOrEmpty(src.Genre) || x.Genre == src.Genre)
                                  && (string.IsNullOrEmpty(src.AgeRating) || x.Rating == Enum.Parse<DataEnums.Rating>(src.AgeRating))),
                   OrderType = src.OrderType,
                   OrderPropertyName = src.OrderPropertyName,
                   Limit = src.Limit,
                   Offset = src.Offset
               });

            CreateMap<OrderDTO, Order>().ReverseMap();

            CreateMap<OrderExchange, OrderDTO>().ReverseMap();

            CreateMap<OrderListExchange, OrderListDTO>().ReverseMap();

            CreateMap<CreateOrderListRequest, OrderListDTO>()
                .ForMember(dest => dest.UserClaim, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

            CreateMap<OrderListDTO, OrderList>()
                .ForMember(dest => dest.Id, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.IsFinalized, opt => opt.UseDestinationValue())
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

            CreateMap<OrderList, OrderListDTO>();

        }
    }
}
