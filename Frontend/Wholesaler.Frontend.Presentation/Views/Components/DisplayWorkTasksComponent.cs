using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.Components
{
    internal class DisplayWorkTasksComponent : Component
    {
        private readonly List<WorkTaskDto> _listOfWorkTasks;

        public DisplayWorkTasksComponent(List<WorkTaskDto> listOfWorktasks)
        {
            _listOfWorkTasks = listOfWorktasks;
        }

        public override void Render()
        {
            Console.WriteLine($"Tasks");

            foreach (var task in _listOfWorkTasks)
            {
                Console.WriteLine(
                    $"\nId: {task.Id}" +
                    $"\nUser id: {task.UserId}" +
                    $"\nRow: {task.Row}" +
                    $"\nIs started: {task.IsStarted}" +
                    $"\nIsFinished: {task.IsFinished}");
            }

            Console.ReadLine();
        }
    }
}
