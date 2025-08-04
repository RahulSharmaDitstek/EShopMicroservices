var builder = WebApplication.CreateBuilder(args);
builder.Services.AddYearpApiGatewayServices(builder.Configuration);

var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();
app.UseCors("AllowBlazorWasm"); // Add this before routing/proxy
app.Run();
