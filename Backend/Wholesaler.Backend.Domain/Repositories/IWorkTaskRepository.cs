using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IWorkTaskRepository
    {
        Guid Add(WorkTask worktask);

        WorkTask Get(Guid id);

        WorkTask Update(WorkTask worktask);

        List<WorkTask> GetNotAssign();

        List<WorkTask> GetAssigned(Guid userId);

        List<WorkTask> GetAssigned();

        List<WorkTask> GetStarted();

        List<WorkTask> GetFinished();
    }
}
