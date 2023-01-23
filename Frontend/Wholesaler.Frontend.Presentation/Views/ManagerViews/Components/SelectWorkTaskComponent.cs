using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews.Components
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
            Console.WriteLine("----------------------------");
            Console.WriteLine("Tasks:");

            foreach (var task in _workTasks)
                Console.WriteLine($"{_workTasks.IndexOf(task) + 1} {task.Id}");

            Console.WriteLine("----------------------------");
            Console.WriteLine("Enter an index of a task you want to assign: ");
            if (!int.TryParse(Console.ReadLine(), out int workTaskNumber))
                throw new InvalidDataProvidedException("You entered an invalid value.");

            var index = workTaskNumber - 1;
            var workTask = _workTasks
                .Where(x => _workTasks.IndexOf(x) == index)
                .FirstOrDefault();

            if (workTask == null)
                throw new InvalidDataProvidedException("You input an invalid value.");

            return workTask;
        }
    }
}
