using App_Repositories.Categories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App_Repositories.Product;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // App_Repositories katmanı altındaki IEntityTypeConfiguration implement eden tüm sınıfları al ef geç demiş olduk.
        base.OnModelCreating(modelBuilder);
    }

    //(auditlog da updated-created kolonlarını tutabilmek amaç)
    //birinci yol : savechanges ezmek
    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //{
    //    //bu iyi bir yöntem değil,
    //    return base.SaveChangesAsync(cancellationToken);
    //}

}
