
using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain
{
    public interface IUserService
    {
        Person Login(string username, string password);
        
        Guid StartWorkday(Guid userId);
    }
}
