using Importacion.Worker.Presunciones;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<CheckPresuncionFilesUploadedWorker>();

builder.Services.UsePresuncionesModule();
builder.Services.UseImagenesModule();

var host = builder.Build();
host.Run();

