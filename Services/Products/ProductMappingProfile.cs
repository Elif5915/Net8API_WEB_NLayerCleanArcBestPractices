using App_Repositories.Product;
using App_Services.Products.Dto;
using App_Services.Products.Dto.Create;
using App_Services.Products.Dto.Update;
using AutoMapper;

namespace App_Services.Products;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        //! aşağıdakilerde nedne reverse yapmadık çünkü  dto dan entity dönüştürme var sadece. requestten entity(product) mapleme yapıyopruz

        //dest : destination(hedef) product karşılık geliyor. src ise gelen CreateProductRequest dir.
        CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant())); // gelen name küçük harflerle db kayıt etsin ve culture lardan etkilenmesin diye tolowerınvariant kullan.
        CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

    }
}
