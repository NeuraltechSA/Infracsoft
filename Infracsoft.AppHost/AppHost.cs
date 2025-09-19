using Aspire.Hosting;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("Db").WithPgAdmin();

// https://learn.microsoft.com/en-us/dotnet/aspire/extensibility/custom-hosting-integration?tabs=windows
var maildev = builder.AddMailDev("MailDev");

var storage = builder.AddAzureStorage("Storage");
if (builder.Environment.IsDevelopment())
{
    storage.RunAsEmulator();
}

var blobs = storage.AddBlobs("BlobConnection");


#region Importación service
var importacionDb = db.AddDatabase("importacion-db");

var importacionWorker = builder.AddProject<Projects.Importacion_Worker>("importacion-worker")
    .WithReference(importacionDb)
    .WaitFor(importacionDb)
    .WithReference(blobs)
    .WaitFor(blobs);

var importacionMigrations = builder.AddProject<Projects.Importacion_Migrations>("importacion-migrations");
#endregion



builder.Build().Run();
