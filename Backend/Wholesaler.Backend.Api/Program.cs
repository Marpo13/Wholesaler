using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DBConnection");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WholesalerContext>(opt => opt.UseSqlServer(connection));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IWorkdayRepository, WorkdayRepository>();
builder.Services.AddTransient<IWorkTaskRepository, WorkTaskRepository>();
builder.Services.AddTransient<IWorkTaskService, WorkTaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
