using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IWorkTaskService
    {
        WorkTask Assign(Guid workTaskId, Guid userId);

        WorkTask ChangeOwner(Guid workTaskId, Guid newOwnerId);

        WorkTask Start(Guid workTaskId);

        WorkTask Stop(Guid workTaskId);
    }
}
