using Microsoft.EntityFrameworkCore;
using Wholesaler.Backend.Api;
using Wholesaler.Backend.Api.Factories;
using Wholesaler.Backend.DataAccess;
using Wholesaler.Backend.DataAccess.Factories;
using Wholesaler.Backend.DataAccess.Repositories;
using Wholesaler.Backend.Domain.Factories;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Services;
using Wholesaler.Barckend.Domain.Providers;
using IWorkTaskFactoryDomain = Wholesaler.Backend.Domain.Factories.Interfaces.IWorkTaskFactory;
using WorkTaskFactoryDomain = Wholesaler.Backend.Domain.Factories.WorkTaskFactory;
using IWorkTaskFactoryDataAccess = Wholesaler.Backend.DataAccess.Factories.IWorkTaskFactory;
using WorkTaskFactoryDataAccess = Wholesaler.Backend.DataAccess.Factories.WorkTaskFactory;

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
builder.Services.AddTransient<IWorkTaskFactoryDomain, WorkTaskFactoryDomain>();
builder.Services.AddTransient<IWorkTaskFactoryDataAccess, WorkTaskFactoryDataAccess>();
builder.Services.AddTransient<IWorkdayFactory, WorkdayFactory>();
builder.Services.AddTransient<IPersonDbFactory, PersonDbFactory>();
builder.Services.AddTransient<IWorkTaskDtoFactory, WorkTaskDtoFactory>();
builder.Services.AddTransient<IWorkdayDtoFactory, WorkdayDtoFactory>();
builder.Services.AddTransient<IUserDtoFactory, UserDtoFactory>();
builder.Services.AddTransient<IPersonDbFactory, PersonDbFactory>();
builder.Services.AddTransient<IPersonFactory, PersonFactory>();
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<ITimeProvider, TimeProvider>();
builder.Services.AddTransient<ErrorHandlingMiddleware>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{

}
