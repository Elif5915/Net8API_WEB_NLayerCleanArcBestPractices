using App_Repositories.Generic;

namespace App_Repositories.Categories;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductAsync(int id);
    IQueryable<Category?> GetCategoryByProducts();
}
