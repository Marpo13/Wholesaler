using Microsoft.EntityFrameworkCore;
using System.Data;
using Wholesaler.Backend.Domain.Interfaces;
namespace Wholesaler.Backend.DataAccess
{
    public class Transaction : ITransaction
    {
        private readonly WholesalerContext _context;

        public bool IsStarted { get; private set; }

        public Transaction(WholesalerContext context)
        {
            _context = context;
        }

        public void Begin()
        {
            _context.Database.BeginTransaction(IsolationLevel.Serializable);
            IsStarted = true;
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
            IsStarted = false;
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
            IsStarted = false;
        }
    }
}
