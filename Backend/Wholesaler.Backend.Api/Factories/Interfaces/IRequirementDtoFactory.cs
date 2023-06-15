﻿using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories.Interfaces
{
    public interface IRequirementDtoFactory
    {
        RequirementDto Create(Requirement requirement);
    }
}
