using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Backend.Domain.Repositories;

namespace Wholesaler.Backend.DataAccess.Repositories
{
    public class WholesalerRepository : IUsersRepository
    {
        public Person GetUser(string login)
        {
            var context = new WholesalerContext();
            var user = context.People
                .Where(p => p.Login == login)
                .First();

            return new Person(user.Id, user.Login, user.Password);
        }
    }
}
