﻿using Wholesaler.Backend.Api.Factories.Interfaces;
using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories;

public class WorkTaskDtoFactory : IWorkTaskDtoFactory
{
    public WorkTaskDto Create(WorkTask workTask)
    {
        return new()
        {
            Id = workTask.Id,
            Row = workTask.Row,
            UserId = workTask.Person?.Id,
            IsStarted = workTask.IsStarted,
            IsFinished = workTask.IsFinished
        };
    }
}
