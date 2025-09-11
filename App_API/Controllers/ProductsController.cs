using App_Services.Products;
using App_Services.Products.Dto;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var serviceResult = await productService.GetAllListAsync();

        //if (productResult.IsSuccess)
        //{
        //    return Ok(productResult.Data);
        //}
        //else
        //{
        //    return BadRequest(productResult.ErrorMessage);
        //}
        return CreateActionResult(serviceResult);
    }

    //2.yol yazımı yukarıdakinden daha sadece tek satırlık
    //public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());

    [HttpGet("{pageNumber:int}/{pageSize:int}")] //rout constraint ler ile rout datalara  ne bekliyorsam onu belirtiyoruz
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
    {
        var serviceResult = await productService.GetPagedAllListAsync(pageNumber,pageSize);

        return CreateActionResult(serviceResult);
    }

    /// <summary>
    /// http://localhost:5000/api/products?id=2 dersek aşağıdaki metod çalışır,datayı query stringde bekler
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productResult = await productService.GetProductByIdAsync(id);

        //if(productResult.StatusCode == HttpStatusCode.NoContent)
        //{
        //    //var result = new ObjectResult(product) { StatusCode = (int)product.StatusCode }; veya aşağıdaki gibi yazılabilir cast etmek istemezsen
        //    //var result = new ObjectResult(productResult) { StatusCode = productResult.StatusCode.GetHashCode() };
        //    return  new ObjectResult(null) { StatusCode = productResult.StatusCode.GetHashCode() };
        //}

        //return new ObjectResult(productResult) { StatusCode = productResult.StatusCode.GetHashCode()};
        return CreateActionResult(productResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateAsync(request));

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

    //[HttpPatch("/stock")] swaggerde neyi güncellediimiz daha net oslun diye buradan özelleştirme yapabiliriz isimlendirme 
    [HttpPatch("stock")] // api/Products/stock şeklinde görülür swagger da 
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));

    //[HttpPut("updateStock")]
    //public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));

}
//rout constraint ler ile isteklerimizde datat ne bekliyorsa onunla kısıtlıyoruz ve 404 not found dönmesini sağlıyoruz. 
//best practies olarak bu daha uygun eğer sen constraint uygulamazsan ama int bekleyen bir endpointe string deper yazıp
//gönderirsen postmande 400 bad request döner ama 404 dönmesi gerek. Swaggerda beklenen paramtre dışında başka bir şey 
//gönderilmiyor onun default kontrol kuralı var. ama postmande o kural yok ondan sen api lerinde endpointlerinin ne beklediğini
//belirterek 400 den 404 hatasına çevirebilirsin response.