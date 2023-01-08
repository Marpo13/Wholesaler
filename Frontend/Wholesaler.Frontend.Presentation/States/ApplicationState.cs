using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States.Interfaces;

namespace Wholesaler.Frontend.Presentation.States
{
    internal class ApplicationState : IState
    {
        private UserDto? _loggedUser;
        private EmployeeViews? _employeeViews;

        public void Initialize()
        {
            _loggedUser = null;
            _employeeViews = null;
        }

        public void Login(UserDto user)
        {
            if (_loggedUser != null)
                throw new InvalidApplicationStateException("User is already logged in.");

            _loggedUser = user;
        }

        public UserDto GetLoggedInUser()
        {
            if (_loggedUser == null)
                throw new InvalidApplicationStateException($"User is not logged in.");

            return _loggedUser;
        }

        public EmployeeViews GetEmployeeViews()
        {
            if (_employeeViews == null)
            {
                _employeeViews = new EmployeeViews();
                _employeeViews.Initialize();
            }

            return _employeeViews;
        }
    }

    internal class EmployeeViews : IState
    {
        public StartWorkdayState? StartWorkday { get; private set; }

        public void Initialize()
        {
            StartWorkday = null;
        }

        public StartWorkdayState GetStartWorkday()
        {
            if(StartWorkday == null)
            {
                StartWorkday= new StartWorkdayState();
                StartWorkday.Initialize();
            }

            return StartWorkday;
        }
    }

    internal class StartWorkdayState : IState
    {
        public Guid? StartedWorkdayId { get; private set; }
        public WorkdayDto? Workday { get; private set; }

        public void Initialize()
        {
            StartedWorkdayId = null;
            Workday = null;
        }

        public void StartWork(Guid workdayId)
        {
            StartedWorkdayId = workdayId;
        }

        public void StartWorkday(WorkdayDto workday)
        {
            Workday = workday;
        }

        public WorkdayDto GetWorkday()
        {
            if (Workday == null)
                throw new InvalidApplicationStateException("Workday is null");

            return Workday;
        }

        public Guid GetStartedWorkdayId()
        {
            if (StartedWorkdayId == null)
                throw new InvalidApplicationStateException("Id is null.");

            return StartedWorkdayId.Value;
        }
    }
}
