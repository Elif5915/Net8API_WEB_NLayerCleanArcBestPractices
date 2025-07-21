using App_Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace App_Repositories.Product;
public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
      return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}
