using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Requests.WorkTasks;

namespace Wholesaler.Backend.Domain.Factories
{
    public class WorkTaskFactory : IWorkTaskFactory
    {
        public WorkTask Create(CreateWorkTaskRequest request)
        {
            if (request.Row == 0)
                throw new InvalidDataProvidedException("You need to provide row value.");

            var workTask = new WorkTask(request.Row);

            return workTask;
        }
    }
}
