using Wholesaler.Backend.Domain.Entities;

namespace Wholesaler.Backend.Domain.Responses.Requirements;

public class GetByCustomFiltersResponse
{
    public GetByCustomFiltersResponse(List<Requirement> requirements, List<string> errors)
    {
        Requirements = requirements;
        Errors = errors;
    }

    public List<Requirement> Requirements { get; }
    public List<string> Errors { get; }
}
