using App_Repositories;
using App_Repositories.Categories;
using App_Services.Categories.Dto;
using App_Services.Categories.Dto.Create;
using App_Services.Categories.Dto.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App_Services.Categories;
public  class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper): ICategoryService
{
    //crud Operation
    public async Task<ServiceResult<int>> Create(CreateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
        if(anyCategory)
        {
            return ServiceResult<int>.Fail("Bu kategori ismi sistemde bulunmaktadır.", System.Net.HttpStatusCode.NotFound);
        }

        var newCategory = new Category
        {
            Name = request.Name,
        };

        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.Success(newCategory.Id);

    }

    public async Task<ServiceResult> Update(int id, UpdateCategoryRequest request)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult.Fail("Güncellenmek istenen kategori bulunamadı.", System.Net.HttpStatusCode.NotFound);
        }

        var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name && x.Id != category.Id).AnyAsync();
        if (isCategoryNameExist)
        {
            return ServiceResult.Fail("Kategori ismi veritabanında bulunmaktadır.", System.Net.HttpStatusCode.BadRequest);
        }

        category = mapper.Map(request, category);

        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> Delete(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
        {
            return ServiceResult.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
        }

        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetAllList()
    {
        var categories = await categoryRepository.GetAll().ToListAsync();
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if(category is null)
        {
            return ServiceResult<CategoryDto>.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto);

    }

    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductAsync(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductAsync(categoryId);
        if(category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail("Kategori Bulunamadı.", HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);

    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryByProducts()
    {
        var category = await categoryRepository.GetCategoryByProducts().ToListAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }

}
