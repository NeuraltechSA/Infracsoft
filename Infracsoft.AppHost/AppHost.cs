using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("db").WithPgAdmin();
// https://learn.microsoft.com/en-us/dotnet/aspire/extensibility/custom-hosting-integration?tabs=windows
var maildev = builder.AddMailDev("maildev");


#region Importación service
var importacionDb = db.AddDatabase("importacion-db");
var importacionWorker = builder
    .AddProject<Projects.Importacion_Worker>("importacion-worker")
    .WithReference(importacionDb);
#endregion



builder.AddProject<Projects.Importacion_Migrations>("importacion-migrations");


builder.Build().Run();
