using App_Repositories.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App_Services.Filters;
public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //buraya yazılan kodlar action metod çalışmadan önce
        var idValue = context.ActionArguments.Values.FirstOrDefault(); //ActionArguments = controllerdaki metodlarımızın aldığı parametrelerdir.

        //gerçekten id key var mı yok mu öğrenmek için
        var idkey = context.ActionArguments.Keys.First();

        if (idValue == null && idkey == "id")
        {
            await next();//ilgili bir sonraki isteği yap yani endpointi çalıştır
            return; //yolculuğa devam et
        }

        //if (!int.TryParse(idValue.ToString(), out var id)) {
        //    await next();
        //    return;
        //}

        if(idValue is not TId id)
        {
            await next();
            return;
        }

        var hasEntity = await genericRepository.AnyAsync(id);

        if (!hasEntity)
        {
            var entityName = typeof(T).Name; // hangi enetity geldiği aldık, product mı yoksa category mi?

            //action method name full path 
            // var actionName = context.ActionDescriptor.DisplayName;

            //Sadece action method ismi örn; Delete gibi.
            var actionName = context.ActionDescriptor.RouteValues["action"];

            var result = ServiceResult.Fail($"Data Bulunamamıştır.({entityName})({actionName}).");

            //context.Result = new NotFoundObjectResult(new { Message = "Entity not found" });
            context.Result = new NotFoundObjectResult(result);
            return;
        }

        await next();
        //buraya yazılan kodlar ise action metod çalıştıktan sonra
    }
}
