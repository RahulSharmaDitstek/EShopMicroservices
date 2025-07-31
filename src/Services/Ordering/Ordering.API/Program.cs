using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri")!);
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("OrderingSQLDbVault")!);
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.

builder.Services.AddOrderingApplicationServices(builder.Configuration)
    .AddOrderingInfrastructureServices(builder.Configuration)
    .AddOrderingWebServices(builder.Configuration);

var app = builder.Build();


//Configure the HTTP request pipeline.

app.UseOrderingAPIServices();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.Run();
