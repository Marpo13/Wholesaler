using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories;

public class WorkdayDtoFactory : IWorkdayDtoFactory
{
    public WorkdayDto Create(Workday workday)
    {
        return new()
        {
            Id = workday.Id,
            Start = workday.Start,
            Stop = workday.Stop
        };
    }
}
