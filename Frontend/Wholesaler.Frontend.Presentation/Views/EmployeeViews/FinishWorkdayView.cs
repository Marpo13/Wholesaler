using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class FinishWorkdayView : View
    {
        private readonly IUserService _service;
        private readonly FinishWorkdayState _state;        

        public FinishWorkdayView(IUserService service, ApplicationState state)
            : base(state)
        {
            _service = service;
            _state = state.GetEmployeeViews().GetFinishWorkday();
            _state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            var id = State.GetLoggedInUser().Id;
            var finishWorking = await _service.FinishWorkingAsync(id);

            if (finishWorking.IsSuccess)
                _state.FinishWork(finishWorking.Payload);

            else
                throw new InvalidDataProvidedException(finishWorking.Message);

            var workdayId = _state.GetFinishedWorkdayId();

            var finishWorkday = await _service.GetWorkdayAsync(workdayId);

            if (finishWorkday.IsSuccess)
                _state.FinishWorkday(finishWorkday.Payload);

            else
                throw new InvalidDataProvidedException(finishWorkday.Message);

            Console.WriteLine($"You finished your work at: {_state.GetWorkday().Stop}");
            Console.ReadLine();
        }
    }
}
