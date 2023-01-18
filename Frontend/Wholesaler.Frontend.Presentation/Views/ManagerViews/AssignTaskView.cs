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
            state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            var userId = State.GetLoggedInUser().Id;
            var workTaskId = _state.GetWorkTaskId();
            var assignTask = await _service.AssignTask(userId, workTaskId);

            if (assignTask.IsSuccess)
                _state.AssignTask(assignTask.Payload);

            else
                throw new InvalidDataProvidedException(assignTask.Message);

            Console.WriteLine($"You assigned task: {assignTask.Payload.Id} to person: {assignTask.Payload.UserId}");
            Console.ReadLine();
        }
    }
}
