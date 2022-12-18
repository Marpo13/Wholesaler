using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain;
using Wholesaler.Backend.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DBConnection");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WholesalerContext>(opt => opt.UseSqlServer(connection));
builder.Services.AddTransient<IUserService, WholesalerService>();
builder.Services.AddTransient<IUsersRepository, WholesalerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
