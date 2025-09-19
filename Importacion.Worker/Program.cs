using Infracsoft.Importacion.Infraestructure.SharedKernel.Persistence.EFCore.Contexts;
using Infracsoft.Importacion.Worker.Imagenes;
using Infracsoft.Importacion.Worker.Presunciones;
using Infracsoft.SharedKernel.Domain.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Contracts;
using SharedKernel.Infraestructure.Events;
using SharedKernel.Infraestructure.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Azure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<CheckPresuncionFilesUploadedWorker>();

builder.Services.UsePresuncionesModule();
builder.Services.UseImagenesModule();

builder.AddAzureBlobServiceClient("BlobConnection");


#region Persistence
builder.Services.AddDbContext<ImportacionDbContext>(options =>
    /*options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres")
    )*/
    options.UseInMemoryDatabase("ImportacionDb")
 );
builder.Services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<ImportacionDbContext>());
#endregion

#region Event Bus Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddLogging(cfg => cfg.AddConsole());
    x.AddEntityFrameworkOutbox<ImportacionDbContext>(o =>
    {
        //o.UsePostgres();
        //o.UseBusOutbox();

    });
    x.UsingInMemory((context, config) =>
    {
        config.ConfigureEndpoints(context);
        //config.Host("localhost", "/", h => { });
    });
});
builder.Services.AddTransient<IEventBus, EventBus>();
#endregion

#region SharedKernel services
builder.Services.AddScoped<IDecompressor, ZipDecompressor>();
#endregion


var host = builder.Build();
host.Run();

