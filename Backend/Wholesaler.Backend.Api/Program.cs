using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUserService, WholesalerService>();
builder.Services.AddTransient<IUsersRepository, WholesalerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
