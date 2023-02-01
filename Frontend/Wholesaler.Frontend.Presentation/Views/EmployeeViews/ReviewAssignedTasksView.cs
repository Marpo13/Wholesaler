using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class ReviewAssignedTasksView : View
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly GetAssignedTasksState _state;

        public ReviewAssignedTasksView(ApplicationState state, IWorkTaskRepository workTaskRepository) : base(state)
        {
            _workTaskRepository = workTaskRepository;
            _state = state.GetEmployeeViews().GetAssigned();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var id = State.GetLoggedInUser().Id;
            var getTasks = await _workTaskRepository.GetAssignedTaskToAnEmployee(id);

            if (getTasks.IsSuccess)
                _state.GetTasks(getTasks.Payload);

            else
                throw new InvalidDataProvidedException(getTasks.Message);

            Console.WriteLine($"Assigned tasks to an employee with id: {id}");

            foreach (var task in getTasks.Payload)
            {                
                Console.WriteLine(
                    $"\nId: {task.Id}" +
                    $"\nRow: {task.Row}");
            }

            Console.ReadLine();
        }
    }
}
