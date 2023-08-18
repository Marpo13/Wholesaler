using System.Xml.Linq;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.DataAccess.Http;
using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.DataAccess
{
    public class WholesalerClient : RequestService,
        IUserService,
        IWorkDayRepository,
        IWorkTaskRepository,
        IUserRepository,
        IRequirementRepository,
        IClientRepository,
        IStorageRepository
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

        public async Task<ExecutionResultGeneric<WorkdayDto>> StartWorkingAsync(Guid userId)
        {
            var request = new Request<StartWorkdayRequestModel, WorkdayDto>()
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
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkdayDto>> FinishWorkingAsync(Guid userId)
        {
            var request = new Request<FinishWorkdayRequestModel, WorkdayDto>()
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

        public async Task<ExecutionResultGeneric<WorkTaskDto>> AssignTaskAsync(Guid workTaskId, Guid userId)
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

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetNotAssignWorkTasksAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/unassigned",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/assigned",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<UserDto>>> GetEmployeesAsync()
        {
            var request = new Request<HttpRequestMessage, List<UserDto>>()
            {
                Path = $"{apiPath}/employees",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskToAnEmployeeAsync(Guid userId)
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/assignedToAnEmployee?userId={userId}",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> ChangeOwnerAsync(Guid workTaskId, Guid newOwnerId)
        {
            var request = new Request<ChangeOwnerRequestModel, WorkTaskDto>()
            {
                Path = $"{apiPath}/worktasks/{workTaskId}/actions/changeOwner",
                Method = HttpMethod.Patch,
                Content = new ChangeOwnerRequestModel()
                {
                    NewOwnerId = newOwnerId,
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> StartWorkTaskAsync(Guid workTaskId)
        {
            var request = new Request<Guid, WorkTaskDto>()
            {
                Path = $"{apiPath}/worktasks/{workTaskId}/actions/start",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> StopWorkTaskAsync(Guid workTaskId)
        {
            var request = new Request<Guid, WorkTaskDto>()
            {
                Path = $"{apiPath}/worktasks/{workTaskId}/actions/stop",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> FinishWorkTaskAsync(Guid workTaskId)
        {
            var request = new Request<Guid, WorkTaskDto>()
            {
                Path = $"{apiPath}/worktasks/{workTaskId}/actions/finish",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetStartedWorkTasksAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/started",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetFinishedWorkTasksAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{apiPath}/worktasks/finished",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<RequirementDto>> Add(int quantity, Guid clientId, Guid storageId)
        {
            var request = new Request<AddRequirementRequestModel, RequirementDto>()
            {
                Path = $"{apiPath}/requirements",
                Method = HttpMethod.Post,
                Content = new AddRequirementRequestModel()
                {
                    ClientId = clientId,
                    Quantity = quantity,
                    StorageId = storageId
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<RequirementDto>> EditQuantity(Guid id, int quantity)
        {
            var request = new Request<UdpateRequirementRequestModel, RequirementDto>()
            {
                Path = $"{apiPath}/requirements/{id}",
                Method = HttpMethod.Patch,
                Content = new UdpateRequirementRequestModel()
                {
                    Quantity = quantity
                }
            };

            return await SendAsync(request);
        }
        public async Task<ExecutionResultGeneric<List<RequirementDto>>> GetAllRequirements()
        {
            var request = new Request<HttpRequestMessage, List<RequirementDto>>()
            {
                Path = $"{apiPath}/requirements",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<RequirementDto>>> GetRequirements(Guid storageId)
        {
            var request = new Request<HttpRequestMessage, List<RequirementDto>>()
            {
                Path = $"{apiPath}/requirements/withStorageId?storageId={storageId}",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<RequirementDto>>> GetRequirementsByStatus(string status)
        {
            var request = new Request<HttpRequestMessage, List<RequirementDto>>()
            {
                Path = $"{apiPath}/requirements/byStatus?status={status}",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<RequirementDto>> CompleteRequirement(Guid id)
        {
            var request = new Request<Guid, RequirementDto>()
            {
                Path = $"{apiPath}/requirements/{id}/actions/complete",
                Method = HttpMethod.Patch
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<ClientDto>>> GetAllClients()
        {
            var request = new Request<HttpRequestMessage, List<ClientDto>>()
            {
                Path = $"{apiPath}/clients",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<StorageDto>> Add(string name)
        {
            var request = new Request<AddStorageRequestModel, StorageDto>()
            {
                Path = $"{apiPath}/storages",
                Method = HttpMethod.Post,
                Content = new AddStorageRequestModel()
                {
                    Name = name
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<StorageDto>>> GetAllStorages()
        {
            var request = new Request<HttpRequestMessage, List<StorageDto>>()
            {
                Path = $"{apiPath}/storages",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<StorageDto>> Deliver(Guid id, int quantity)
        {
            var request = new Request<UpdateStorageRequestModel, StorageDto>()
            {
                Path = $"{apiPath}/storages/{id}/actions/deliver",
                Method = HttpMethod.Patch,
                Content = new UpdateStorageRequestModel()
                {
                    Quantity = quantity
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<int>> GetCosts()
        {
            var request = new Request<HttpRequestMessage, int>()
            {
                Path = $"{apiPath}/storages/costs",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }
    }
}

