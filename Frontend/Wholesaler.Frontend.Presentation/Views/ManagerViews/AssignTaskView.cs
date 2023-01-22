using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews
{
    internal class AssignTaskView : View
    {
        private readonly IUserService _service;
        private readonly AssignTaskState _state;

        public AssignTaskView(IUserService service, ApplicationState state) : base(state)
        {
            _service = service;
            _state = State.GetManagerViews().GetAssignTask();
            _state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            CheckRole();

            var userId = await GetUserIdAsync();

            var workTaskId = await GetWorkTaskIdAsync();
           
            var assignTask = await _service.AssignTask(workTaskId, userId);

            if (assignTask.IsSuccess)
                _state.AssignTask(assignTask.Payload);

            else
                throw new InvalidDataProvidedException(assignTask.Message);

            Console.WriteLine("----------------------------");
            Console.WriteLine($"You assigned task: {assignTask.Payload.Id} to person: {assignTask.Payload.UserId}");
            Console.ReadLine();
        }

        protected void CheckRole()
        {
            var role = State.GetLoggedInUser().Role;

            if (role != "Manager")
                throw new InvalidOperationException($"You can not assign task with role {role}. Valid role is Manager.");
        }

        protected async Task<Guid> GetWorkTaskIdAsync()
        {
            var listOfWorkTasks = await _service.GetNotAssignWorkTasks();

            if (listOfWorkTasks.IsSuccess)
                _state.AssignTasks(listOfWorkTasks.Payload);

            Console.WriteLine("----------------------------");
            Console.WriteLine("Not assigned tasks:");

            foreach (var task in listOfWorkTasks.Payload)
                Console.WriteLine($"{listOfWorkTasks.Payload.IndexOf(task) + 1} {task.Id}");
            
            Console.WriteLine("----------------------------");
            Console.WriteLine("Enter an index of a task you want to assign: ");
            if (!int.TryParse(Console.ReadLine(), out int workTaskNumber))
                throw new InvalidDataProvidedException("You entered an invalid value.");

            var index = workTaskNumber - 1;
            var workTask = listOfWorkTasks.Payload
                .Where(x => listOfWorkTasks.Payload.IndexOf(x) == index)
                .FirstOrDefault();

            if (workTask == null)
                return default;

            var workTaskId = workTask.Id;            

            return workTaskId;
        }

        protected async Task<Guid> GetUserIdAsync()
        {
            var listOfEmployees = await _service.GetEmployees();

            if (listOfEmployees.IsSuccess)
                _state.GetEmployees(listOfEmployees.Payload);

            Console.WriteLine("----------------------------");
            Console.WriteLine("Employees ID:");

            foreach (var employee in listOfEmployees.Payload)
                Console.WriteLine($"{listOfEmployees.Payload.IndexOf(employee) + 1}: {employee.Id}");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Enter an id of an employee you want to choose: ");
            if (!int.TryParse(Console.ReadLine(), out int userNumber))
                throw new InvalidDataProvidedException("You entered an invalid value.");

            var index = userNumber - 1;
            var user = listOfEmployees.Payload
                .Where(x => listOfEmployees.Payload.IndexOf(x) == index)
                .FirstOrDefault();

            if (user == null)
                return default;

            var userId = user.Id;

            return userId;
        }      
    }
}
