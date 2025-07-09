using Catalog.API.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<CatalogInitialDataClass>(options =>
        options.UseSqlServer(connString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            sqlOptions.CommandTimeout(120);
        }));

builder.Services.AddCatalogServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.Run();
