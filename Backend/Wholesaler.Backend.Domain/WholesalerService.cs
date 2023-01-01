using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Exceptions;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.Domain
{
    public class WholesalerService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public WholesalerService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Person LogByLogin(string loginFromUser, string passwordFromUser)
        {
            var personWithLoginFromUser = _usersRepository.GetUserOrDefault(loginFromUser);

            if (personWithLoginFromUser == null)
                throw new InvalidDataProvidedException($"There is no person with login: {loginFromUser}.");

            if (personWithLoginFromUser.Password != passwordFromUser)
                throw new InvalidDataProvidedException("You entered an invalid password.");

            return personWithLoginFromUser;
        }

        public Guid StartWorkday(Guid userId)
        {
            var person = _usersRepository.GetUserOrDefault(userId);
            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");
            var time = DateTime.Now;
            var workday = new Workday(time, person);

            var createdWorkday = _usersRepository.AddWorkday(workday);

            return createdWorkday.Id;
        }
    }
}
