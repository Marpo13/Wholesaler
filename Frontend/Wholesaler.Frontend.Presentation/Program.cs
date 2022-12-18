// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wholesaler.Frontend.DataAccess;
using Wholesaler.Frontend.Domain;

Console.WriteLine("Login: ");
var login = Console.ReadLine();
Console.WriteLine("Password: ");
var password = Console.ReadLine();
Console.WriteLine("Enter OK to continue.");
var input = Console.ReadLine();

var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddTransient<Login>();
    services.AddTransient<IUserService, HttpRequest>();
});

var app = host.Build();
var menu = ActivatorUtilities.CreateInstance<Login>(app.Services);

if (input == "OK")
{    
    var loginResult = await menu.LoginWithDataFromUserAsync(login, password);
    if (loginResult.IsSuccess)
        Console.WriteLine("You are logged in.");
    else
    {
        Console.WriteLine(loginResult.Message);
        Console.WriteLine("Login: ");
        login = Console.ReadLine();
        Console.WriteLine("Password: ");
        password = Console.ReadLine();
        Console.WriteLine("Enter OK to continue.");
        input = Console.ReadLine();
        await menu.LoginWithDataFromUserAsync(login, password);
    }

    Console.ReadLine();
}

