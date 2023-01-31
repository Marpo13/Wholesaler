using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.DataAccess.Http;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.DataAccess
{
    public class WholesalerClient : RequestService, IUserService
    {
        private const string apiPath = $"http://localhost:5050";

        public async Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            var request = new Request<LoginUserRequestModel, UserDto>()
            {
                Path = $"{apiPath}/users/actions/login",
                Method = HttpMethod.Post,
                Content = new LoginUserRequestModel
                {
                    Login = loginFromUser,
                    Password = passwordFromUser,
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<Guid>> StartWorkingAsync(Guid userId)
        {
            var request = new Request<StartWorkdayRequestModel, Guid>()
            {
                Path = $"{apiPath}/workdays/actions/start",
                Method = HttpMethod.Post,
                Content = new StartWorkdayRequestModel()
                {
                    UserId = userId,
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkdayDto>> GetWorkdayAsync(Guid workdayid)
        {
            var request = new Request<HttpRequestMessage, WorkdayDto>()
            {
                Path = $"{apiPath}/workdays/{workdayid}",
                Method = HttpMethod.Get,
                Content = new HttpRequestMessage(),
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<Guid>> FinishWorkingAsync(Guid userId)
        {
            var request = new Request<FinishWorkdayRequestModel, Guid>()
            {
                Path = $"{apiPath}/workdays/actions/finish",
                Method = HttpMethod.Post,
                Content = new FinishWorkdayRequestModel()
                {
                    UserId = userId,
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> AssignTask(Guid workTaskId, Guid userId)
        {
            var request = new Request<AssignTaskRequestModel, WorkTaskDto>()
            {
                Path = $"{apiPath}/worktasks/{workTaskId}/actions/assign",
                Method = HttpMethod.Post,
                Content = new AssignTaskRequestModel()
                {
                    UserId = userId,                    
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetNotAssignWorkTasks()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/workTasks/unassigned",
                Method = HttpMethod.Get,
                Content = new HttpRequestMessage(),
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<UserDto>>> GetEmployees()
        {
            var request = new Request<HttpRequestMessage, List<UserDto>>()
            {
                Path = $"{apiPath}/employees",
                Method = HttpMethod.Get,
                Content = new HttpRequestMessage(),
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskToAnEmployee(Guid userId)
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/action/assignedToAnEmployee?userId={userId}",
                Method = HttpMethod.Get,
                Content = new HttpRequestMessage(),
            };

            return await SendAsync(request);
        }
    }
}

