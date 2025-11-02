using App_Repositories.Product;
using App_Services.Products.Dto;
using AutoMapper;

namespace App_Services.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile() {
        CreateMap<Product, ProductDto>().ReverseMap();
    
    }
}
