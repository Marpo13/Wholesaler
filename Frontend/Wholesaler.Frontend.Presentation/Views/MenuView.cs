using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Interfaces;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views
{
    internal class MenuView : View, IMenuView
    {
        private readonly ApplicationState _state;
        private readonly IUserService _service;

        public MenuView(ApplicationState state, IUserService service)
        {
            _state = state;
            _service = service;
        }

        protected override async Task RenderView()
        {
            var role = _state.User.Role;

            switch (role)
            {
                case "Employee":
                    EmployeeView();
                    break;

                case "Manager":
                    ManagerView();
                    break;

                case "Owner":
                    OwnerView();
                    break;

                default: throw new Exception($"User login: {_state.User.Login} has invalid role: {role}.");
            }
        }

        private void EmployeeView()
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

                if (pressedKey.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    Console.WriteLine("Enter your id");
                    var wasIdProvided = Guid.TryParse(Console.ReadLine(), out Guid userId);
                    if (!wasIdProvided)
                    {
                        Console.WriteLine("You entered an invaild id.");
                        continue;
                    }
                    _service.StartWorking(userId);

                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    Console.WriteLine("Enter your id");
                    var wasIdProvided = Guid.TryParse(Console.ReadLine(), out Guid userId);
                    if (!wasIdProvided)
                    {
                        Console.WriteLine("You entered an invaild id.");
                        continue;
                    }
                    _service.StartWorking(userId);

                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    continue;
                }
            }
        }

        private void ManagerView()
        {
            var pressedKey = Console.ReadKey();

            while (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.Write("---Welcome in Wholesaler---");
                Console.WriteLine
                    ("\n[1] To assign a row of mushrooms to an employee" +
                    "\n[2] To change owner of a task" +
                    "\n[3] To enter customer requirement" +
                    "\n[4] To edit customer requirement" +
                    "\n[5] To get information about ended tasks" +
                    "\n[6] To get information about started tasks" +
                    "\n[7] To see progress of requirement" +
                    "\n[ESC] To quit");

                if (pressedKey.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D3)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D4)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D5)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D6)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D7)
                {
                    Console.Clear();
                    continue;
                }
            }
        }

        private void OwnerView()
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

                if (pressedKey.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    continue;
                }

                else if (pressedKey.Key == ConsoleKey.D2)
                {
                    {
                    }
                }
            }
        }
    }
}

