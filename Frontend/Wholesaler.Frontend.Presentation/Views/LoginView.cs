using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Interfaces;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views
{
    internal class LoginView : View, ILoginView
    {
        private readonly IUserService _service;        

        public LoginView(IUserService service, ApplicationState state)
            : base(state)
        {
            _service = service;            
        }

        protected override async Task RenderViewAsync()
        {
            bool isLoggedSucccesfully = false;

            while (isLoggedSucccesfully is false)
            {
                Console.WriteLine("Login: ");
                var login = Console.ReadLine();
                Console.WriteLine("Password: ");
                var password = Console.ReadLine();
                Console.WriteLine("Enter OK to continue.");
                var input = Console.ReadLine();

                if (input == "OK")
                {
                    var loginResult = await _service.TryLoginWithDataFromUserAsync(login, password);

                    if (loginResult.IsSuccess)
                    {
                        State.Login(loginResult.Payload);
                        break;
                    }
                }
            }

        }
    }
}
