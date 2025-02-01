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
        IStorageRepository,
        IDeliveryRepository
    {
        private const string ApiPath = "http://localhost:5050";

        public async Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            var request = new Request<LoginUserRequestModel, UserDto>()
            {
                Path = $"{ApiPath}/users/actions/login",
                Method = HttpMethod.Post,
                Content = new()
                {
                    Login = loginFromUser,
                    Password = passwordFromUser
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkdayDto>> StartWorkingAsync(Guid userId)
        {
            var request = new Request<StartWorkdayRequestModel, WorkdayDto>()
            {
                Path = $"{ApiPath}/workdays/actions/start",
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
                Path = $"{ApiPath}/workdays/{workdayid}",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkdayDto>> FinishWorkingAsync(Guid userId)
        {
            var request = new Request<FinishWorkdayRequestModel, WorkdayDto>()
            {
                Path = $"{ApiPath}/workdays/actions/finish",
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
                Path = $"{ApiPath}/worktasks/{workTaskId}/actions/assign",
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
                Path = $"{ApiPath}/worktasks/unassigned",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{ApiPath}/worktasks/assigned",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<UserDto>>> GetEmployeesAsync()
        {
            var request = new Request<HttpRequestMessage, List<UserDto>>()
            {
                Path = $"{ApiPath}/employees",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetAssignedTaskToAnEmployeeAsync(Guid userId)
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{ApiPath}/worktasks/assignedToAnEmployee?userId={userId}",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> ChangeOwnerAsync(Guid workTaskId, Guid newOwnerId)
        {
            var request = new Request<ChangeOwnerRequestModel, WorkTaskDto>()
            {
                Path = $"{ApiPath}/worktasks/{workTaskId}/actions/changeOwner",
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
                Path = $"{ApiPath}/worktasks/{workTaskId}/actions/start",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> StopWorkTaskAsync(Guid workTaskId)
        {
            var request = new Request<Guid, WorkTaskDto>()
            {
                Path = $"{ApiPath}/worktasks/{workTaskId}/actions/stop",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<WorkTaskDto>> FinishWorkTaskAsync(Guid workTaskId)
        {
            var request = new Request<Guid, WorkTaskDto>()
            {
                Path = $"{ApiPath}/worktasks/{workTaskId}/actions/finish",
                Method = HttpMethod.Post,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetStartedWorkTasksAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{ApiPath}/worktasks/started",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<WorkTaskDto>>> GetFinishedWorkTasksAsync()
        {
            var request = new Request<HttpRequestMessage, List<WorkTaskDto>>()
            {
                Path = $"{ApiPath}/worktasks/finished",
                Method = HttpMethod.Get,
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<RequirementDto>> Add(int quantity, Guid clientId, Guid storageId)
        {
            var request = new Request<AddRequirementRequestModel, RequirementDto>()
            {
                Path = $"{ApiPath}/requirements",
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
                Path = $"{ApiPath}/requirements/{id}",
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
                Path = $"{ApiPath}/requirements",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<RequirementDto>>> GetRequirements(Guid storageId)
        {
            var request = new Request<HttpRequestMessage, List<RequirementDto>>()
            {
                Path = $"{ApiPath}/requirements/withStorageId?storageId={storageId}",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<RequirementDto>>> GetRequirementsByStatus(string status)
        {
            var request = new Request<HttpRequestMessage, List<RequirementDto>>()
            {
                Path = $"{ApiPath}/requirements/byStatus?status={status}",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<RequirementDto>> CompleteRequirement(Guid id)
        {
            var request = new Request<Guid, RequirementDto>()
            {
                Path = $"{ApiPath}/requirements/{id}/actions/complete",
                Method = HttpMethod.Patch
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<List<ClientDto>>> GetAllClients()
        {
            var request = new Request<HttpRequestMessage, List<ClientDto>>()
            {
                Path = $"{ApiPath}/clients",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<StorageDto>> Add(string name)
        {
            var request = new Request<AddStorageRequestModel, StorageDto>()
            {
                Path = $"{ApiPath}/storages",
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
                Path = $"{ApiPath}/storages",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<StorageDto>> Deliver(Guid id, int quantity, Guid personId)
        {
            var request = new Request<UpdateStorageRequestModel, StorageDto>()
            {
                Path = $"{ApiPath}/storages/{id}/actions/deliver",
                Method = HttpMethod.Patch,
                Content = new UpdateStorageRequestModel()
                {
                    Quantity = quantity,
                    PersonId = personId
                }
            };

            return await SendAsync(request);
        }

        public async Task<ExecutionResultGeneric<float>> GetCosts(long from, long to)
        {
            var request = new Request<HttpRequestMessage, float>()
            {
                Path = $"{ApiPath}/raports/costs?from={from}&to={to}",
                Method = HttpMethod.Get
            };

            return await SendAsync(request);
        }
    }
}

