using App_Repositories.Generic;
using App_Repositories.Product;
using Microsoft.EntityFrameworkCore;

namespace App_Repositories.Categories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public IQueryable<Category?> GetCategoryByProducts()
    { 
        return Context.Categories.Include(x => x.Products).AsQueryable(); //AsQueryable yaptık ki ilerde order by ile sıralama yapmak istersek yapabilelim diye.
    }

    public Task<Category?> GetCategoryWithProductAsync(int id)
    {
       return Context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }
}
