using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class ReviewAssignedTasksView : View
    {
        private readonly IUserService _service;
        private readonly GetAssignedTasksState _state;

        public ReviewAssignedTasksView(ApplicationState state, IUserService service) : base(state)
        {
            _service = service;
            _state = state.GetEmployeeViews().GetAssigned();
            _state.Initialize();
        }

        protected async override Task RenderViewAsync()
        {
            var id = State.GetLoggedInUser().Id;
            var getTasks = await _service.GetAssignedTaskToAnEmployee(id);

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
