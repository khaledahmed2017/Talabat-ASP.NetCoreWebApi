using AutoMapper;
using StackExchange.Redis;
using Talabat.DTOs;
using TalabatCore.Entities;
using TalabatCore.Entities.Identity;
using TalabatCore.Entities.OrderAggregate;

namespace Talabat.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToDto>()
                //             ProductDto      ,                        Product
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandToReturnDTO>();
            CreateMap<ProductType, TypeToReturnDTo>();
            CreateMap<TalabatCore.Entities.Identity.Address, AddressDto>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, TalabatCore.Entities.OrderAggregate.Address>();

            CreateMap<TalabatCore.Entities.OrderAggregate.Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodShortName, o => o.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(S => S.ProductItemOrder.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(S => S.ProductItemOrder.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(S => S.ProductItemOrder.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderPictureUrlResolver>());

        }
    }
}
