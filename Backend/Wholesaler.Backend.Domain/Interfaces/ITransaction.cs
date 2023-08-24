namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface ITransaction
    {
        bool IsStarted { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
