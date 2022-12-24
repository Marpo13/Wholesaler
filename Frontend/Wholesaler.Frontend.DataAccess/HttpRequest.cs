using Newtonsoft.Json;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.DataAccess
{
    public class HttpRequest : IUserService
    {
        public async Task<ExecutionResult<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .GetAsync($"http://localhost:5050/users?login={loginFromUser}&password={passwordFromUser}");

                var description = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var person = JsonConvert.DeserializeObject<UserDto>(description);
                    return ExecutionResult<UserDto>.CreateSuccessful(person);
                }

                return ExecutionResult<UserDto>.CreateFailed(description);
            }
        }
        
    }
}