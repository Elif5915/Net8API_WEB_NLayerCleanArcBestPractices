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
        });

        return services;
    }
}
