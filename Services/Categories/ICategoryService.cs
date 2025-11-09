using App_Services.Categories.Dto;
using App_Services.Categories.Dto.Create;
using App_Services.Categories.Dto.Update;

namespace App_Services.Categories;
public  interface ICategoryService
{
    Task<ServiceResult<int>> Create(CreateCategoryRequest request);

    Task<ServiceResult> Update(int id, UpdateCategoryRequest request);

    Task<ServiceResult> Delete(int id);

    Task<ServiceResult<List<CategoryDto>>> GetAllList();

    Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);

    Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductAsync(int categoryId);

    Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryByProducts();
}
