// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.DataAccess;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views;


var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddTransient<IUserService, HttpRequest>();
    services.AddTransient<ILoginView, LoginView>();
    services.AddTransient<IMenuView, MenuView>();
    services.AddSingleton<ApplicationState>();
    });

var app = host.Build();
//var login = ActivatorUtilities.CreateInstance<LoginView>(app.Services);
//await login.Render();

var state = app.Services.GetRequiredService<ApplicationState>();
state.User = new UserDto
{
    Id = Guid.NewGuid(),
    Login = "Maja",
    Role = "Employee"
};

var menuView = ActivatorUtilities.CreateInstance<MenuView>(app.Services);
await menuView.Render();
//TODO: display menu based on role



