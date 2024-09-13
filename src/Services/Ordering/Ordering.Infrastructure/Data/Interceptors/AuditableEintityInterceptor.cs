

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEintityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public void UpdateEntities(DbContext? context)
        {
            if (context == null)
            {
                return;
            }
            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "sahin";
                    entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);

                }
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangeOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = "shahin";
                    entry.Entity.LastModified = DateTime.UtcNow.AddHours(4);


                }
            }
        }
    }
    public static class Extensions
    {
        public static bool HasChangeOwnedEntities(this EntityEntry entry)
        {
            return

            entry.References.Any(r=>
            r.TargetEntry!=null&&
            r.TargetEntry.Metadata.IsOwned()
            &&(r.TargetEntry.State==EntityState.Added||r.TargetEntry.State==EntityState.Modified));
        }
    }
}
