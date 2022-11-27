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

        public void LogByLogin(string loginFromUser, string passwordFromUser)
        {
            var personWithLoginFromUser = _usersRepository.GetUser(loginFromUser);
            
            if(personWithLoginFromUser.Password == passwordFromUser)
                Console.WriteLine("Ok");

            else
                Console.WriteLine("You entered an invalid values.");
        }
    }
}
