using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App_Repositories.Interceptors;
public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> _behaviors = new()
    {
        {EntityState.Added,AddBehavior},
        {EntityState.Modified,ModifiedBehavior}
    };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventdata, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventdata.Context!.ChangeTracker.Entries().ToList())
        {

            if (entityEntry.Entity is not IAuditEntity auditEntity)
            {
                continue; //burada gelen entity IAuditEntity implement almamışssa onu bırak devam et sıradaki entity geç 
            }

            _behaviors[entityEntry.State](eventdata.Context, auditEntity);

            //switch (entityEntry.State)
            //{
            //    case EntityState.Added:

                   
            //        break;

            //    case EntityState.Modified:


                  
            //        break;
            //}

        }

        return base.SavingChangesAsync(eventdata, result, cancellationToken);
    }

    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x => x.Updated).IsModified = false; //bu data güncellenmemiş demiş olduk, ef core sql sorguna updated alma dedik
    }
    private static void ModifiedBehavior(DbContext context,IAuditEntity auditEntity)
    {
        auditEntity.Updated = DateTime.Now;
        context.Entry(auditEntity).Property(x => x.Created).IsModified = false; // ef core Created alanıma dokunma değiştirme yapılmayacak demiş olduk.

    }
}
