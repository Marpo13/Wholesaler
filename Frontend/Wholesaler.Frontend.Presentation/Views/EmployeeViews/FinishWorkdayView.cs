using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.EmployeeViews
{
    internal class FinishWorkdayView : View
    {
        private readonly IWorkDayRepository _workDayRepository;
        private readonly IUserService _service;
        private readonly FinishWorkdayState _state;        

        public FinishWorkdayView(IUserService service, IWorkDayRepository workDayRepository, ApplicationState state)
            : base(state)
        {
            _service = service;
            _workDayRepository = workDayRepository;
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

            var finishWorkday = await _workDayRepository.GetWorkdayAsync(workdayId);

            if (finishWorkday.IsSuccess)
                _state.FinishWorkday(finishWorkday.Payload);

            else
                throw new InvalidDataProvidedException(finishWorkday.Message);

            Console.WriteLine($"You finished your work at: {_state.GetWorkday().Stop}");
            Console.ReadLine();
        }
    }
}
