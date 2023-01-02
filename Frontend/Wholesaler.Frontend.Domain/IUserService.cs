using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain.ValueObjects;
using static Wholesaler.Frontend.Domain.ValueObjects.ExecutionResultGeneric;


namespace Wholesaler.Frontend.Domain
{
    public interface IUserService
    {
        Task<ExecutionResult<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser);
       
    }
}
