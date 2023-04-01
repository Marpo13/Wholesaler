using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Factories.Interfaces;
using Wholesaler.Backend.Domain.Interfaces;
using Wholesaler.Backend.Domain.Providers.Interfaces;
using Wholesaler.Backend.Domain.Repositories;
using Wholesaler.Backend.Domain.Requests.People;

namespace Wholesaler.Backend.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IWorkdayRepository _workdayRepository;
        private readonly IPersonFactory _personFactory;
        private readonly ITimeProvider _timeProvider;

        public UserService(
            IUsersRepository usersRepository,
            IWorkdayRepository workdayRepository,
            ITimeProvider timeProvider,
            IPersonFactory personFactory)
        {
            _timeProvider = timeProvider;
            _usersRepository = usersRepository;
            _workdayRepository = workdayRepository;
            _personFactory = personFactory;
        }

        public Person Login(string loginFromUser, string passwordFromUser)
        {
            var user = _usersRepository.GetOrDefault(loginFromUser);

            if (user == null)
                throw new InvalidDataProvidedException($"There is no person with login: {loginFromUser}.");

            if (user.Password != passwordFromUser)
                throw new InvalidDataProvidedException("You have entered an invalid password.");

            return user;
        }

        public Workday StartWorkday(Guid userId)
        {
            var person = _usersRepository.GetOrDefault(userId);
            var time = _timeProvider.Now();

            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");

            if (person.Role != Role.Employee)
                throw new InvalidDataProvidedException($"You can not create a workday for role: {person.Role}. You have to be an Employee.");

            var activeWorkday = _workdayRepository.GetActiveByPersonOrDefaultAsync(userId);

            if (activeWorkday != null)
                throw new InvalidDataProvidedException($"You can not start another workday, because you started workday with Id: {activeWorkday.Id}");

            var workday = new Workday(time, person);
            var createdWorkday = _workdayRepository.Add(workday);

            return createdWorkday;
        }


        public Workday FinishWorkday(Guid userId)
        {
            var person = _usersRepository.GetOrDefault(userId);
            var time = _timeProvider.Now();

            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");

            var activeWorkday = _workdayRepository.GetActiveByPersonOrDefaultAsync(userId);

            if (activeWorkday == null)
                throw new InvalidDataProvidedException($"There is no started workdays for person with id: {userId}");

            activeWorkday.StopWorkday(time);
            _workdayRepository.UpdateWorkday(activeWorkday);

            return activeWorkday;
        }

        public Person Add(CreatePersonRequest request)
        {
            var person = _personFactory.Create(request);
            _usersRepository.AddPerson(person);

            return person;
        }
    }
}
