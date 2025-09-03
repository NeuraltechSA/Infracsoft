using MassTransit;
using Microsoft.EntityFrameworkCore;
using Renombrado.API.Modules.Fuentes.Extensions;
using Renombrado.Fuentes.Application.CreateFuenteFtp;
using Renombrado.SharedKernel.Infraestructure.Persistence.EFCore.Contexts;
using SharedKernel.Domain.Contracts;
using SharedKernel.Infraestructure.Events;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

#region Modules
builder.Services.UseFuentesModule();
#endregion

#region Persistence
builder.Services.AddDbContext<RenombradoDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres")
    )
 );
 builder.Services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<RenombradoDbContext>());
#endregion

#region Event Bus Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<FuenteFtpCreatedEventConsumer>();
    x.AddLogging(cfg => cfg.AddConsole());
    x.AddEntityFrameworkOutbox<RenombradoDbContext>(o =>
    {
        o.UsePostgres();
        o.UseBusOutbox();

    });
    x.UsingInMemory((context, config) =>
    {
        config.ConfigureEndpoints(context);
        //config.Host("localhost", "/", h => { });
    });
});

builder.Services.AddTransient<IEventBus, EventBus>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();