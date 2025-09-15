using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RenombradoOld.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((context, services) =>
{
    services.AddDbContext<RenombradoDbContext>(options =>
        options.UseNpgsql(
            context.Configuration.GetConnectionString("Default"),
            x => x.MigrationsAssembly("Renombrado.Migrations")
        ));
});

IHost host = builder.Build();

using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<RenombradoDbContext>();


//dbContext.Database.EnsureDeleted();
//dbContext.Database.EnsureCreated();

//host.Run();
