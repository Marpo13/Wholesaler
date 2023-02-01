// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wholesaler.Frontend.DataAccess;
using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.Interfaces;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views;
using Wholesaler.Frontend.Presentation.Views.EmployeeViews;
using Wholesaler.Frontend.Presentation.Views.ManagerViews;
using Wholesaler.Frontend.Presentation.Views.UsersViews;

var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddTransient<IUserService, WholesalerClient>();
    services.AddTransient<IWorkDayRepository, WholesalerClient>();
    services.AddTransient<IWorkTaskRepository, WholesalerClient>();
    services.AddTransient<IUserRepository, WholesalerClient>();
    services.AddTransient<ILoginView, LoginView>();
    services.AddTransient<IMenuView, MenuView>();
    services.AddTransient<EmployeeView>();
    services.AddTransient<ManagerView>();
    services.AddTransient<OwnerView>();
    services.AddTransient<StartWorkdayView>();
    services.AddTransient<FinishWorkdayView>();
    services.AddTransient<AssignTaskView>();
    services.AddTransient<ReviewAssignedTasksView>();
    services.AddSingleton<ApplicationState>();
    });

var app = host.Build();
var state = ActivatorUtilities.CreateInstance<ApplicationState>(app.Services);
state.Initialize();

var login = ActivatorUtilities.CreateInstance<LoginView>(app.Services);
await login.RenderAsync();

var menuView = ActivatorUtilities.CreateInstance<MenuView>(app.Services);
await menuView.RenderAsync();




