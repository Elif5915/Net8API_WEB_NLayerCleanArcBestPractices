using App_Services.Products.Dto;

namespace App_Services.Categories.Dto;
public record CategoryWithProductsDto(int id,string Name,List<ProductDto> Products);
