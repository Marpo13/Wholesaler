using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.Components
{
    public class SelectWorkTaskComponent : Component<WorkTaskDto>
    {
        private readonly List<WorkTaskDto> _workTasks;

        public SelectWorkTaskComponent(List<WorkTaskDto> workTasks)
        {
            _workTasks = workTasks;
        }

        public override WorkTaskDto Render()
        {
            bool wasCorrectValueProvided = false;
            WorkTaskDto? workTask = null;

            while (wasCorrectValueProvided is false)
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.WriteLine("Tasks:");

                foreach (var task in _workTasks)
                    Console.WriteLine($"{_workTasks.IndexOf(task) + 1} {task.Id}");

                Console.WriteLine("----------------------------");
                Console.WriteLine("Enter an index of a task you want to choose: ");
                if (!int.TryParse(Console.ReadLine(), out int workTaskNumber))
                {
                    Console.WriteLine("You entered an invalid value.");
                    continue;
                }

                var index = workTaskNumber - 1;
                workTask = _workTasks
                    .Where(x => _workTasks.IndexOf(x) == index)
                    .FirstOrDefault();

                if (workTask == null)
                {
                    Console.WriteLine("You entered an invalid value.");
                    continue;
                }

                wasCorrectValueProvided = true;
            }

            return workTask;
        }
    }
}
