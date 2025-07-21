using App_Repositories.Product;
using App_Services.Products.Dto;
using System.Net;

namespace App_Services.Products;
public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = (await productRepository.GetTopPriceProductsAsync(count));

        var productsDto = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price * 1.2m, 

        }).ToList(); //manuel mapping en hızlısı amna biz automapper kullanarakdaha sonra yapacağız


        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsDto
        };
        //return new ServiceResult<List<Product>>()
        //{
        //    Data = products

        //};
    }
    public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if(product is null)
        {
            //return new ServiceResult<Product>()
            //{
            //    ErrorMessage = new List<string>() { "Product not found" }
            //};
            ServiceResult<Product>.Fail(errorMessage: "Product not found", HttpStatusCode.NotFound);
        }
        //return new ServiceResult<Product>()
        //{
        //    Data = product
        //};
        return ServiceResult<Product>.Success(product!);
    }

}
