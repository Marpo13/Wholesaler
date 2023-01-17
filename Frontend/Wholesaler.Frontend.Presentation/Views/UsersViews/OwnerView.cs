using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class OwnerView : View
    {
        private readonly IUserService _service;

        public OwnerView(IUserService service, ApplicationState state)
            : base(state)
        {
            _service = service;
        }

        protected override async Task RenderViewAsync()
        {
            var pressedKey = Console.ReadKey();

            while (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.Write("---Welcome in Wholesaler---");
                Console.WriteLine
                    ("\n[1] To check costs" +
                    "\n[2] To check incomes" +
                    "\n[3] To see balance sheet" +
                    "\n[ESC] To quit");

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        continue;

                    case ConsoleKey.Escape:
                        break;

                    default: continue;
                }
            }
        }
    }
}
