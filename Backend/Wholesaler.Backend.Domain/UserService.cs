using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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

        public Guid StartWorkday(Guid userId)
        {
            var person = _usersRepository.GetUserOrDefault(userId);
            var time = DateTime.Now;

            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");

            var workday = _usersRepository.GetWorkdayOrDefault(userId);

            if (workday == null)
            {
                var newWorkday = new Workday(time, person);
                var createdWorkday = _usersRepository.AddWorkday(newWorkday);
                return createdWorkday.Id;
            }

            else
            {
                if (workday.Stop == null)
                    throw new InvalidDataProvidedException($"You can not start another workday, because you already started workday with Id: {workday.Id}");

                var newWorkday = new Workday(time, person);
                var createdWorkday = _usersRepository.AddWorkday(newWorkday);
                return createdWorkday.Id;
            }           
            
        }

        public Guid FinishWorkday(Guid userId)
        {
            var person = _usersRepository.GetUserOrDefault(userId);            

            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");

            var workday = _usersRepository.GetWorkdayOrDefault(userId);

            if (workday == null)
            {
                throw new InvalidDataProvidedException($"There is no started workdays for person with id: {userId}");
            }

            else
            {
                if (workday.Stop == null)
                {
                    workday.StopWorkday();
                    _usersRepository.UpdateWorkday(workday.Id, workday.Stop);
                }
                
                else
                    throw new InvalidDataProvidedException($"Workday with id {workday.Id} is already ended.");

                return workday.Id;
            }

        }
    }
}
