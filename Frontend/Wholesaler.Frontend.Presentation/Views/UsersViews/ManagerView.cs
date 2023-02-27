using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Generic;
using Wholesaler.Frontend.Presentation.Views.ManagerViews;

namespace Wholesaler.Frontend.Presentation.Views.UsersViews
{
    internal class ManagerView : View
    {        
        private readonly AssignTaskView _assignTask;
        private readonly ChangeOwnerOfTaskView _changeOwner;
        private readonly StartedTasksView _startedTasks;
        private readonly FinishedTasksView _finishedTasks;

        public ManagerView(ApplicationState state, AssignTaskView assignTask, ChangeOwnerOfTaskView changeOwner, StartedTasksView startedTasks, FinishedTasksView finishedTasks)
            : base(state)
        {
            _assignTask = assignTask;
            _changeOwner = changeOwner;
            _startedTasks = startedTasks;
            _finishedTasks = finishedTasks;
        }

        protected override async Task RenderViewAsync()
        {
            var wasExitKeyPressed = false;

            while (wasExitKeyPressed == false)
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

                var pressedKey = Console.ReadKey();

                switch (pressedKey.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        await _assignTask.RenderAsync();
                        continue;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        await _changeOwner.RenderAsync();
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
                        await _finishedTasks.RenderAsync();
                        continue;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        await _startedTasks.RenderAsync();
                        continue;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
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
