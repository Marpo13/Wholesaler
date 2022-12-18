
using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain
{
    public interface IUserService
    {
        Person LogByLogin(string username, string password);
    }
}
