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
            if (person == null)
                throw new InvalidDataProvidedException($"There is no person with id: {userId}");
            var time = DateTime.Now;
            var workday = new Workday(time, person);

            var createdWorkday = _usersRepository.AddWorkday(workday);

            return createdWorkday.Id;
        }
    }
}
