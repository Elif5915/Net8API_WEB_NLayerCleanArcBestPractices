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
        if(result.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode()};
        }

        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode()};
    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
        }

        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }
}
