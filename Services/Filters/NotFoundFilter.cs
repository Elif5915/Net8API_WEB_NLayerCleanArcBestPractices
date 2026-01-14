using App_Repositories.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App_Services.Filters;
internal class NotFoundFilter<T> : Attribute, IAsyncActionFilter where T : class
{
    public NotFoundFilter(IGenericRepository<T> genericRepository)
    {

    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //buraya yazılan kodlar action metod çalışmadan önce
        var idValue = context.ActionArguments.Values.FirstOrDefault(); //ActionArguments = controllerdaki metodlarımızın aldığı parametrelerdir.
        if (idValue == null)
        {
            await next();//ilgili bir sonraki isteği yap yani endpointi çalıştır
            return; //yolculuğa devam et
        }
            


        await next();
        //buraya yazılan kodlar ise action metod çalıştıktan sonra
    }
}
