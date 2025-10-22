using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App_Services;
public  class FluentValidationFilter : IAsyncActionFilter //FluentValidationFilter  bu filterı global olarak eklememiz lazım ki her yerde çalışssın
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors) //burada gelen collectionımızı tek kullanabilceğimiz listeye çevirdik.
                .Select(y => y.ErrorMessage).ToList();

            var resultModel = ServiceResult.Fail(errors);

            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }

        await next(); //eğer herhangi bir hata yok request bizim servis/metodumuza girebilirsin ilerle diyoruz.
    }
}
