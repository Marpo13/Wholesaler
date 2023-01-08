using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Repositories
{
    public interface IWorkdayRepository
    {
        Workday Add(Workday workday);      

        Workday? GetActiveOrDefault(Guid id);

        List<Workday> GetByPersonAsync(Guid personId);
    }
}
