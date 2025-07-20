using App_Repositories.Generic;
using App_Repositories.Product;

namespace App_Services;
public class ProductService(IGenericRepository<Product> productRepository)
{
}
