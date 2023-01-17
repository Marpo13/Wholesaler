using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IUsersRepository _usersRepository;

        public WorkTaskService(IWorkTaskRepository workTaskRepository, IUsersRepository usersRepository)
        {
            _workTaskRepository = workTaskRepository;
            _usersRepository = usersRepository;
        }

        public WorkTask Assign(Guid workTaskId, Guid userId)
        {
            var workTask = _workTaskRepository.GetOrDefault(workTaskId);
            var person = _usersRepository.GetUserOrDefault(userId);
            if (person.Role != Role.Employee)
                throw new InvalidDataProvidedException($"You can not assign task to {person.Role}. Valid role is employee");

            workTask.AssignPerson(person);

            _workTaskRepository.Update(workTask);

            return workTask;
        }
    }
}
