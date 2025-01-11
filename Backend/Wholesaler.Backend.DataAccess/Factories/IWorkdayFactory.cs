using Wholesaler.Backend.Domain.Entities;
using WorkdayDb = Wholesaler.Backend.DataAccess.Models.Workday;

namespace Wholesaler.Backend.DataAccess.Factories;

public interface IWorkdayFactory
{
    Workday Create(WorkdayDb workdayDb);
}
