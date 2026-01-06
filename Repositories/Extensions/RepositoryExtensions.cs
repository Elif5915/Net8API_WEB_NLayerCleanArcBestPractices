using App_Repositories.Categories;
using App_Repositories.Generic;
using App_Repositories.Interceptors;
using App_Repositories.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App_Repositories.Extensions;
public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            #region 1.yol
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "SqlServer"));
            //});
            #endregion

            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName); //mig dosyalarım repository katmanımda olmalı olsun diye bu ayar yazıldı
            });

            options.AddInterceptors(new AuditDbContextInterceptor());

        });

        services.AddScoped<IProductRepository, ProductRepository>();//IProductRepository gördüğün zaman ProductRepository nesne örneği üret
        //yaşam döngüsü AddScoped; request response döndüğü anda ProductRepository nesnesinde dispose olması
        //lazım çünkü ProductRepository bu nesnelerde dbcontextler var, dbcontextlerin yaşam döngüsü scopdur.
        //request geldiğinde bir nesne örneği oluşur response döndüğü zaman da dbcontextlerde dispose olur.
        //dispose = elden çıkarmak
        //bu yüzden AddScoped<IProductRepository, ProductRepository> bunlar AddSingleton olmaz ya da AddTransient olmaz AMA EF CORE İÇİN GEÇERLİ!
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
