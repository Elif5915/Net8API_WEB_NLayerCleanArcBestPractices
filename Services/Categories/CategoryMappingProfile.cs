using App_Repositories.Categories;
using App_Services.Categories.Dto;
using App_Services.Categories.Dto.Create;
using App_Services.Categories.Dto.Update;
using AutoMapper;

namespace App_Services.Categories;
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();

        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant())); // gelen name küçük harflerle db kayıt etsin ve culture lardan etkilenmesin diye tolowerınvariant kullan.
       
        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
    }
}
