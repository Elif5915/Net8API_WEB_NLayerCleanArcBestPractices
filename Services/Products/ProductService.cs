using App_Repositories;
using App_Repositories.Product;
using App_Services.Products.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace App_Services.Products;
public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = (await productRepository.GetTopPriceProductsAsync(count));

        var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>> { Data = productDto };


        #region manuel mapleme ile
        //var productsDto = products.Select(p => new ProductDto
        //{
        //    Id = p.Id,
        //    Name = p.Name,
        //    Price = p.Price * 1.2m, 

        //}).ToList(); //manuel mapping en hızlısı amna biz automapper kullanarakdaha sonra yapacağız


        //return new ServiceResult<List<ProductDto>>()
        //{
        //    Data = productsDto
        //};

        #endregion
        //yaptılan manuel map en hızlı çalışır, kullandığınız tğm mapper kütüphanelerinden daha hızlı çalışır.

        //return new ServiceResult<List<Product>>()
        //{
        //    Data = products

        //};
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productDto = products.Select(p => new ProductDto( p.Id,p.Name,p.Price, p.Stock
        )).ToList();

        return ServiceResult<List<ProductDto>>.Success(productDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber,int pageSize)
    {
        // 1(pagenumber) - 10(pagesize) gelirse ilk 10 kayıt demiş oluruz. Bunu yazmak için Skip metodundan faydalanacağız.
        // skip(0).take(10) skip ile sıfırı atla, take ile ilk 10 kaydı al
        //eğerki ikinci sf 10 kayıt ver derse 2 - 10 => 11-20 kayıt arası olmalı => skip(10).take(10) on atla on taneyi ver.

        // 1- 10 => ilk 10 kayıt  skip(0).Take(10)
        // 2- 10 => 11 - 20 kayıt  skip(10).Take(10)
        // 3- 10 => 21 - 30 kayıt  skip(20).Take(10) biz bunu yapmamız gerek.

        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        var productDto = products.Select(p => new ProductDto
        (
            p.Id,p.Name,p.Price,p.Stock

        )).ToList();

        return ServiceResult<List<ProductDto>>.Success(productDto);

    }

    public async Task<ServiceResult<ProductDto?>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if(product is null)
        {
            //return new ServiceResult<Product>()
            //{
            //    ErrorMessage = new List<string>() { "Product not found" }
            //};
            ServiceResult<ProductDto>.Fail(errorMessage: "Product not found", HttpStatusCode.NotFound);
        }
        //return new ServiceResult<Product>()
        //{
        //    Data = product
        //};

        var productsAsDto = new ProductDto(product!.Id,product.Name,product.Price,product.Stock);
        return ServiceResult<ProductDto>.Success(productsAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Stock = request.Stock,
            Price = request.Price
        };
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}");

    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await productRepository.GetByIdAsync(id);

        ///iki tane servislerde kullanmamız gereken clean code prensibi vardır.
        /// 1) Fast fail : önce olumsuz durumları göz önünde tutup en son olumlu duruma bakılmalı.
        /// 2) Guard Claueses : özellikle validasyon tarafında ya da servislerde önce bir gardını al, tüm olumsuz durumları if lerle yaz.
        /// mümkün olduğunca if*else kullanma kodun okunabilirliğini oldukça düşürür else ler.

        if(product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetByIdAsync(request.productId);

        if(product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Stock = request.quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
// Save changes her zaman servis katamında yapılır, repository katmanında yapılmaz.transaction servis katmanından yönetilir.