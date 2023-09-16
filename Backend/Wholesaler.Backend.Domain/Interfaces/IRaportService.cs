﻿namespace Wholesaler.Backend.Domain.Interfaces
{
    public interface IRaportService
    {
        float GetCosts();
        float GetCostsForEmployee(Guid employeeId);
    }
}
