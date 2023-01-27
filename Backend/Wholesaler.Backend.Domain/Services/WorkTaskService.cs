using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain.Services
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
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person != null)
                throw new InvalidDataProvidedException($"Work task with id: {workTaskId} is already assigned to employee: {workTask.Person.Id}");

            var person = _usersRepository.Get(userId);
            if (person.Role != Role.Employee)
                throw new InvalidDataProvidedException($"You can not assign task to {person.Role}. Valid role is employee");

            workTask.AssignPerson(person);

            _workTaskRepository.UpdatePerson(workTask);

            return workTask;
        }

        public WorkTask ChangeOwner(Guid workTaskId, Guid newOwnerId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person == null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not already assigned.");
            if (workTask.Person.Id == newOwnerId)
                throw new InvalidDataProvidedException($"Task with id: {workTask.Id} is already assigned to employee with id: {newOwnerId}");

            var person = _usersRepository.Get(newOwnerId);
            if (person.Role != Role.Employee)
                throw new InvalidDataProvidedException($"You can not assign task to {person.Role}. Valid role is employee");

            workTask.AssignPerson(person);

            _workTaskRepository.UpdatePerson(workTask);

            return workTask;
        }

        public WorkTask Start(Guid workTaskId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person == null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not already assigned and can not be started.");
            if (workTask.Start != null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is already started.");

            workTask.StartWorkTask();
            _workTaskRepository.Start(workTask);

            return workTask;
        }

        public WorkTask Stop(Guid workTaskId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);            
            if (workTask.Stop != null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is already finished.");

            workTask.StopWorkTask();
            _workTaskRepository.Stop(workTask);

            return workTask;
        }
    }
}
