using CodeTest.DAL;
using CodeTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddSingleton<IPackageRepository, PackageRepository>();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
