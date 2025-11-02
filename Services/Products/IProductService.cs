using App_Services.Products.Dto;
using App_Services.Products.Dto.Create;
using App_Services.Products.Dto.Update;
using App_Services.Products.Dto.UpdateStock;

namespace App_Services.Products;
public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);

    Task<ServiceResult<List<ProductDto>>> GetAllListAsync();

    Task<ServiceResult<ProductDto?>> GetProductByIdAsync(int id);

    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);

    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);

    Task<ServiceResult> DeleteAsync(int id);

    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);

    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
}
