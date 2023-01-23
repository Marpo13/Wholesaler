using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IWorkTaskRepository
    {
        Guid Add(WorkTask worktask);

        WorkTask Get(Guid id);

        WorkTask Update(WorkTask worktask);

        List<WorkTask> GetNotAssign();

        List<WorkTask> GetAssign(Guid userId);

        List<WorkTask> GetAssign();
    }
}
