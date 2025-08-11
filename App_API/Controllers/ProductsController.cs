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

    [HttpGet]
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

    [HttpPut]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));

    [HttpDelete]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));

}
