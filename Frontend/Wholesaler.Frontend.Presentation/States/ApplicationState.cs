using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States.Interfaces;

namespace Wholesaler.Frontend.Presentation.States
{
    internal class ApplicationState : IState
    {
        private UserDto? _loggedUser;
        private EmployeeViews? _employeeViews;
        private ManagerViews? _managerViews;

        public void Initialize()
        {
            _loggedUser = null;
            _employeeViews = null;
            _managerViews = null;
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

        public ManagerViews GetManagerViews()
        {
            if(_managerViews == null)
            {
                _managerViews = new ManagerViews();
                _managerViews.Initialize();
            }

            return _managerViews;
        }
    }

    internal class EmployeeViews : IState
    {
        public StartWorkdayState? StartWorkday { get; private set; }
        public FinishWorkdayState? FinishWorkday { get; private set; }
        public GetAssignedTasksState? GetAssignedTasks { get; private set; }

        public void Initialize()
        {
            StartWorkday = null;
            FinishWorkday = null;
            GetAssignedTasks = null;
        }

        public StartWorkdayState GetStartWorkday()
        {
            if (StartWorkday == null)
            {
                StartWorkday = new StartWorkdayState();
                StartWorkday.Initialize();
            }

            return StartWorkday;
        }

        public FinishWorkdayState GetFinishWorkday()
        {
            if(FinishWorkday == null)
            {
                FinishWorkday = new FinishWorkdayState();
                FinishWorkday.Initialize();
            }

            return FinishWorkday;
        }

        public GetAssignedTasksState GetAssigned()
        {
            if(GetAssignedTasks == null)
            {
                GetAssignedTasks = new GetAssignedTasksState();
                GetAssignedTasks.Initialize();
            }

            return GetAssignedTasks;
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

    internal class FinishWorkdayState : IState
    {
        public Guid? FinishedWorkdayId { get; private set; }
        public WorkdayDto? Workday { get; private set; }

        public void Initialize()
        {
            FinishedWorkdayId = null;
            Workday = null;
        }

        public WorkdayDto GetWorkday()
        {
            if (Workday == null)
                throw new InvalidApplicationStateException("Workday is null");

            return Workday;
        }

        public void FinishWork(Guid workdayId)
        {
            FinishedWorkdayId = workdayId;
        }

        public Guid GetFinishedWorkdayId()
        {
            if (FinishedWorkdayId == null)
                throw new InvalidApplicationStateException("Id is null.");

            return FinishedWorkdayId.Value;
        }

        public void FinishWorkday(WorkdayDto workday)
        {
            Workday = workday;
        }
        
    }

    internal class ManagerViews : IState
    {
        public AssignTaskState? AssignTask { get; private set; }

        public void Initialize()
        {
            AssignTask = null;
        }

        public AssignTaskState GetAssignTask()
        {
            if (AssignTask == null)
            {
                AssignTask = new AssignTaskState();
                AssignTask.Initialize();
            }

            return AssignTask;
        }
    }

    internal class AssignTaskState : IState
    {
        public Guid? WorkTaskId { get; private set; }
        public WorkTaskDto? WorkTask { get; private set; }
        public List<WorkTaskDto>? WorkTasks { get; private set; }
        public List<UserDto>? Employees { get; private set; }
               
        public void Initialize()
        {
            WorkTaskId = null;
            WorkTask = null;
        }

        public Guid GetWorkTaskId()
        {
            if (WorkTaskId == null)
                throw new InvalidApplicationStateException("WorkTask id  is null");

            return WorkTaskId.Value;
        }

        public WorkTaskDto GetWorkTask()
        {
            if (WorkTask == null)
                throw new InvalidApplicationStateException("WorkTask is null");

            return WorkTask;
        }

        public void AssignTask(WorkTaskDto workTask)
        {
            WorkTask = workTask;
        }

        public void AssignTasks(List<WorkTaskDto> workTasks)
        {
            WorkTasks = workTasks;
        }

        public void GetEmployees(List<UserDto> employees)
        {
            Employees = employees;
        }
    }

    internal class GetAssignedTasksState : IState
    {
        public List<WorkTaskDto> Worktasks { get; private set; }

        public void Initialize()
        {
            Worktasks = null;
        }

        public void GetTasks(List<WorkTaskDto> workTasks)
        {
            Worktasks = workTasks;
        }
    }
}
