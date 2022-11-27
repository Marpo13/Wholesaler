// See https://aka.ms/new-console-template for more information
using Wholesaler.Frontend.Domain;

Console.WriteLine("Login: ");
var login = Console.ReadLine();
Console.WriteLine("Password: ");
var password = Console.ReadLine();
Console.WriteLine("Enter OK to continue.");
var input = Console.ReadLine();

if(input == "OK")
{
    var verifyLogin = new Login();
    await verifyLogin.LoginWithDataFromUserAsync(login, password);
    Console.WriteLine("You are logged in.");
}

