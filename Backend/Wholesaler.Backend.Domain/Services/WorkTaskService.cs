using System;
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
        private readonly IWorkdayRepository _workdayRepository;

        public WorkTaskService(IWorkTaskRepository workTaskRepository, IUsersRepository usersRepository, IWorkdayRepository workdayRepository)
        {
            _workTaskRepository = workTaskRepository;
            _usersRepository = usersRepository;
            _workdayRepository = workdayRepository;
        }

        public WorkTask Assign(Guid workTaskId, Guid userId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person != null)
                throw new InvalidDataProvidedException($"Work task with id: {workTaskId} is assigned to employee: {workTask.Person.Id}");

            var person = _usersRepository.Get(userId);            
            if (person.Role != Role.Employee)
                throw new UnpermittedActionPerformedException($"You can not assign task to {person.Role}. Valid role is employee");

            workTask.AssignPerson(person);

            _workTaskRepository.Update(workTask);

            return workTask;
        }

        public WorkTask ChangeOwner(Guid workTaskId, Guid newOwnerId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person == null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not assigned.");
            if (workTask.Person.Id == newOwnerId)
                throw new InvalidDataProvidedException($"Task with id: {workTask.Id} is assigned to employee with id: {newOwnerId}");

            var person = _usersRepository.Get(newOwnerId);
            if (person.Role != Role.Employee)
                throw new UnpermittedActionPerformedException($"You can not assign task to {person.Role}. Valid role is employee");

            workTask.AssignPerson(person);

            _workTaskRepository.Update(workTask);

            return workTask;
        }

        public WorkTask Start(Guid workTaskId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.Person == null)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not assigned and can not be started.");

            var activeWorkday = _workdayRepository.GetActiveByPersonOrDefaultAsync(workTask.Person.Id);
            if (activeWorkday == null)
                throw new UnpermittedActionPerformedException("You can not start worktask because you did not start work.");

            workTask.StartStatus();
            _workTaskRepository.Update(workTask);

            return workTask;
        }

        public WorkTask Stop(Guid workTaskId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.IsStarted != true)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not started and can not be stopped.");

            workTask.Stop();
            _workTaskRepository.Update(workTask);

            return workTask;
        }

        public WorkTask Finish(Guid workTaskId)
        {
            var workTask = _workTaskRepository.Get(workTaskId);
            if (workTask.IsStarted != true)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is not started and can not be stopped.");
            if (workTask.IsFinished == true)
                throw new InvalidDataProvidedException($"Task with id {workTaskId} is finished.");

            workTask.FinishStatus();
            _workTaskRepository.Update(workTask);

            return workTask;
        }
    }
}
