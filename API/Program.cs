using API;
using API.Data;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder => builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("https://localhost:4200"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}
catch(Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "Ha ocurrido un error durante el sembrado");
}

app.Run();


// // Test
// using API.Data;
// using Microsoft.EntityFrameworkCore;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container

// builder.Services.AddControllers();
// builder.Services.AddDbContext<DataContext>(opt => {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddCors();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// app.UseCors(builder => builder.AllowAnyHeader()
//                                 .AllowAnyMethod()
//                                 .WithOrigins("http://localhost:4200"));
// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
// app.Run();