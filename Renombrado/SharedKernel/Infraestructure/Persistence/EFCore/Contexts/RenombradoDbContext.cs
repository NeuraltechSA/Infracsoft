using Microsoft.EntityFrameworkCore;
using Renombrado.Fuentes.Infrastructure.Persistence.EFCore.Models;
using SharedKernel.Domain.Contracts;
using SharedKernel.Infraestructure.Persistence.EFCore.Models;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using SharedKernel.Infraestructure.Persistence.EFCore.Services;

namespace Renombrado.SharedKernel.Infraestructure.Persistence.EFCore.Contexts
{
    public class RenombradoDbContext : DbContext, IUnitOfWork
    {
        public DbSet<FuenteModel> Fuentes { get; set; }

        public RenombradoDbContext(DbContextOptions<RenombradoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        public override int SaveChanges()
        {
            EntityTimestampUpdater.UpdateTimestamps(this);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EntityTimestampUpdater.UpdateTimestamps(this);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
