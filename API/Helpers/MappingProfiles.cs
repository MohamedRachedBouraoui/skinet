using API.Dtos;
using AutoMapper;
using Core.Entities;

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
        }
    }
}