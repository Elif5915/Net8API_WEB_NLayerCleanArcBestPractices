using App_Repositories.Generic;

namespace App_Repositories.Product;
public interface IProductRepository : IGenericRepository<Product>
{
     public IEnumerable<Product> GetTopPriceProductsAsync(int count);
}
