using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.WorkTasks;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IWorkTaskService
    {
        WorkTask Assign(Guid workTaskId, Guid userId);

        WorkTask ChangeOwner(Guid workTaskId, Guid newOwnerId);

        WorkTask Start(Guid workTaskId);

        WorkTask Stop(Guid workTaskId);

        WorkTask Finish(Guid workTaskId);

        WorkTask Add(CreateWorkTaskRequest request);
    }
}
