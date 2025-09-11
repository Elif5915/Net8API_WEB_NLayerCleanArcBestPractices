using App_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction] // bu atribute ile bunların bir endpoint olmadığı söylüyoruz compiler. Bunlar yardımcı metodlarımız
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return NoContent();
            // return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode());
        }

        if (result.StatusCode == System.Net.HttpStatusCode.Created)
        {
            //return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode()};
            return Created(result.UrlAsCreated, result.Data);
        }

        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode()};


        //return result.StatusCode switch { System.Net.HttpStatusCode.NoContent => NoContent(),
        //    System.Net.HttpStatusCode.Created => Created(urlAsCreated, result.Data),
        //=> new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
        //}; bu yapıya  switch expression denir .net 8 ile switch-case yeni hali daha kısa hata yapma olasılığı az olması durumu sağlar.

    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
        }

        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
        //switch yapıısnda yazmak istersek aşağıdaki yapı gibi olur;
        //return result.StatusCode switch {
        //    System.Net.HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode()},
        //    _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode()}
        //   };
    }
}
