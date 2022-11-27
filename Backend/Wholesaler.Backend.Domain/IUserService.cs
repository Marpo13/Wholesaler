
namespace Wholesaler.Backend.Domain
{
    public interface IUserService
    {
        void LogByLogin(string username, string password);
    }
}
