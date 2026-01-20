using App_Services.Categories;
using App_Services.ExceptionHandlers;
using App_Services.Filters;
using App_Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App_Services.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); //bu .net in default olarak üretmiş olduğu hata mesajlarını kapattık.

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        //her request geldiğinde NotFoundFilter filterımız çalışsın
        services.AddScoped(typeof(NotFoundFilter<,>)); //NotFoundFilter iki tane generate aldığı için virgül koyduk!

        services.AddFluentValidationAutoValidation(); //fluent validation paketini yükledik ve bu kod ile fluent val. tanı dedik.

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //ekstra validasyon classlarını otomatik tanıyabilmesi için 

        //GetExecutingAssembly ile bu app_services katamanı içinde dahili olarak burada çalış/kapsa demiş oluyoruz.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //exceptionları verirken sıralama önemliymiş!çünkü dönüşlerde true ya da false dönüldüğü için

        services.AddExceptionHandler<CriticalExceptionHandler>(); //bunda false dönüyoruz yani hatayla bir şey yapmıyorum sadece yakalıp onunla ilgili işlemimi yapıyorum örneğin sms gönderiyorum sonra yolculuğuna devam ettriyorum;  bir sonraki exception middleware aktar.
        services.AddExceptionHandler<GlobalExceptionHandler>(); // bunda da true dönüyoruz kendi modelimizi oluşturup onu gönderip olayı bitiyoruz.
        return services;
    }
}
