using Wholesaler.Backend.Domain.Entities;
using WorkTaskDb = Wholesaler.Backend.DataAccess.Models.WorkTask;

namespace Wholesaler.Backend.DataAccess.Factories
{
    public interface IListWorkTaskFactory
    {
        List<WorkTask> Create(List<WorkTaskDb> workTaskDb);
    }
}
