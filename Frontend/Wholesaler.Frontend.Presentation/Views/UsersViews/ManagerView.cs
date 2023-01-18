using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.ManagerViews;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class ManagerView : View
    {        
        private readonly AssignTaskView _assignTask;

        public ManagerView(ApplicationState state, AssignTaskView assignTask)
            : base(state)
        {
            _assignTask = assignTask;
        }

        protected override async Task RenderViewAsync()
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

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        await _assignTask.RenderAsync();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        continue;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
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
