using EasyDonate.Application;
using EasyDonate.Persistence;
using EasyDonate.API.Extensions;
using EasyDonate.Persistence.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddOpenApiWithJwt();

builder.Services.ConfigureCorsPolicy(builder.Configuration);
builder.Services.ConfigureSecurityOptions(builder.Configuration);
builder.Services.ConfigureJsonEnumAsString();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    
var app = builder.Build();

app.ConfigureScalarUI();
app.UseHttpsRedirection();
app.UseCors(app.Environment.IsDevelopment() ? "Development" : "Production");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
