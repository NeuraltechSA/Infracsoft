using Microsoft.EntityFrameworkCore;
using Infracsoft.Importacion.Infraestructure.Presunciones.Persistence.EFCore.Models;
using SharedKernel.Domain.Contracts;
using MassTransit;
using SharedKernel.Infraestructure.Persistence.EFCore.Services;
using Infracsoft.Importacion.Infraestructure.Imagenes.Persistence.EFCore.Models;


namespace Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;

public class ImportacionDbContext : DbContext, IUnitOfWork
{
    public DbSet<PresuncionModel> Presunciones { get; set; }
    public DbSet<ImagenModel> Imagenes { get; set; }

    public ImportacionDbContext(DbContextOptions<ImportacionDbContext> options) : base(options)
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