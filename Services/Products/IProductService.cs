using App_Services.Products.Dto;

namespace App_Services.Products;
public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);

    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();

    Task<ServiceResult<ProductDto?>> GetProductByIdAsync(int id);

    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);

    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);

    Task<ServiceResult> DeleteAsync(int id);
}
