using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IWorkdayRepository _workdayRepository;

        public UserService(IUsersRepository usersRepository, IWorkdayRepository workdayRepository)
        {
            _usersRepository = usersRepository;
            _workdayRepository = workdayRepository;
        }

        public Person Login(string loginFromUser, string passwordFromUser)
        {
            var user = _usersRepository.GetUserOrDefault(loginFromUser);

            if (user == null)
                throw new InvalidDataProvidedException($"There is no person with login: {loginFromUser}.");

            if (user.Password != passwordFromUser)
                throw new InvalidDataProvidedException("You have entered an invalid password.");

            return user;
        }

        public Workday StartWorkday(Guid userId)
        {
            var person = _usersRepository.GetUserOrDefault(userId);
            var time = DateTime.Now;

            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");

            var workdayList = _workdayRepository.GetByPersonAsync(userId);

            if (workdayList == null)
            {
                var newWorkday = CreateNewWorkday(time, person);
                return newWorkday;
            }

            var startedWorkday = workdayList.Find(w => w.Stop == null);

            if(startedWorkday != null)
                throw new InvalidDataProvidedException($"You can not start another workday, because you already started workday with Id: {startedWorkday.Id}");

            var newWorkday1 = CreateNewWorkday(time, person);
            return newWorkday1;            
        }

        public Workday CreateNewWorkday(DateTime time, Person person)
        {
            var newWorkday = new Workday(time, person);
            var createdWorkday = _workdayRepository.Add(newWorkday);

            return createdWorkday;
        }       
    }
}
