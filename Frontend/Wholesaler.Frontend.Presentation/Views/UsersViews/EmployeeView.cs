using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.EmployeeViews;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class EmployeeView : View
    {
        private readonly StartWorkdayView _startWorkday;
        private readonly FinishWorkdayView _finishWorkday;
        private readonly StartWorkTaskView _startWorkTask;
        private readonly WorkTaskMenuView _workTaskMenu;

        public EmployeeView(
            StartWorkdayView startWorkday,
            ApplicationState state,
            FinishWorkdayView finishWorkday,
            StartWorkTaskView startworkTask,
            WorkTaskMenuView workTaskMenu)
            : base(state)
        {
            _startWorkday = startWorkday;
            _finishWorkday = finishWorkday; 
            _startWorkTask = startworkTask;
            _workTaskMenu = workTaskMenu;
        }

        protected override async Task RenderViewAsync()
        {
            var wasExitKeyPressed = false;

            while (wasExitKeyPressed == false)
            {
                Console.WriteLine("---Welcome in Wholesaler---");
                Console.WriteLine
                    ("\n[1] To see worktask menu" +
                    "\n[2] To start work" +
                    "\n[3] To finish work" +
                    "\n[ESC] To quit");

                var pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        await _workTaskMenu.RenderAsync();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        await _startWorkday.RenderAsync();
                        await _startWorkTask.RenderAsync();
                        continue;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        await _finishWorkday.RenderAsync();
                        continue;

                    case ConsoleKey.Escape:
                        wasExitKeyPressed = true;
                        break;

                    default: continue;
                }
            }
        }
    }
}
