var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOrderingApplicationServices()
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
