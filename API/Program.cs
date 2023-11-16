using API;
using API.Extensions;

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