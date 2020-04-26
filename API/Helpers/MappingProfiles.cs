using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(dto => dto.ProductBrand, opt => opt.MapFrom(p => p.ProductBrand.Name))
            .ForMember(dto => dto.ProductType, opt => opt.MapFrom(p => p.ProductType.Name))
            .ForMember(dto => dto.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();    //Identity

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
            .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrdered.ProductItemId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ItemOrdered.ProductName))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemUrlResolver>());
        }
    }
}