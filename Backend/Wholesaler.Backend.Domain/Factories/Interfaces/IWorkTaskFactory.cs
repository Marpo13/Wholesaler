using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Requests.WorkTasks;

namespace Wholesaler.Backend.Domain.Factories.Interfaces
{
    public interface IWorkTaskFactory
    {
        WorkTask Create(CreateWorkTaskRequest request);
    }
}
