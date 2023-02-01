using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain
{
    public interface IWorkTaskService
    {
        WorkTask Assign(Guid workTaskId, Guid userId);
    }
}
