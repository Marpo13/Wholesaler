using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.EmployeeViews;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class EmployeeView : View
    {
        private readonly StartWorkday _startWorkday;

        public EmployeeView(StartWorkday startWorkday, ApplicationState state) 
            : base(state)
        {
            _startWorkday = startWorkday;
        }

        protected override async Task RenderViewAsync()
        {
            var wasExitKeyPressed = false;

            while (wasExitKeyPressed == false)
            {
                Console.WriteLine("---Welcome in Wholesaler---");
                Console.WriteLine
                    ("\n[1] To record ending of the row" +
                    "\n[2] To start work" +
                    "\n[3] To finish work" +
                    "\n[4] To get information about assigned tasks" +
                    "\n[ESC] To quit");

                var pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:                        
                        await _startWorkday.RenderAsync();                       
                        continue;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
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
