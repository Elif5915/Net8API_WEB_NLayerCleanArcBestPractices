using App_Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App_Services.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();

        services.AddFluentValidationAutoValidation(); //fluent validation paketini yükledik ve bu kod ile fluent val. tanı dedik.

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); //ekstra validasyon classlarını otomatik tanıyabilmesi için 

        //GetExecutingAssembly ile bu app_services katamanı içinde dahili olarak burada çalış/kapsa demiş oluyoruz.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
