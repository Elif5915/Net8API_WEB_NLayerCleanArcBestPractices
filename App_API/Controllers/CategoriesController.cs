using App_Services.Categories;
using App_Services.Categories.Dto.Create;
using App_Services.Categories.Dto.Update;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers;
public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCategories() => CreateActionResult(await categoryService.GetAllList());

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request) => CreateActionResult(await categoryService.Create(request));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request) => CreateActionResult(await categoryService.Update(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await categoryService.Delete(id));

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts() => CreateActionResult(await categoryService.GetCategoryByProducts());

}
