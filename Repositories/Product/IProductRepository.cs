using App_Repositories.Generic;

namespace App_Repositories.Product;
public interface IProductRepository : IGenericRepository<Product,int>
{
     public Task<List<Product>> GetTopPriceProductsAsync(int count);
}
