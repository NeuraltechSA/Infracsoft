using Microsoft.EntityFrameworkCore;
using SharedKernel.Infraestructure.Persistence.EFCore.Models;


namespace SharedKernel.Infraestructure.Persistence.EFCore.Services;

public class EntityTimestampUpdater{
    public static void UpdateTimestamps(DbContext context){
        var entries = context.ChangeTracker
                                .Entries()
                                .Where(e => e.Entity is BaseEFCoreModel &&
                                        (e.State == EntityState.Added || e.State == EntityState.Modified));
            
            foreach (var entry in entries)
            {
                var entity = (BaseEFCoreModel)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                    // Prevenir que se modifique CreatedAt
                    entry.Property(nameof(BaseEFCoreModel.CreatedAt)).IsModified = false;
                }
            }
    }
}