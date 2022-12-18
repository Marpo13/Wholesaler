using System.Net;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;
using static System.Net.WebRequestMethods;

namespace Wholesaler.Frontend.DataAccess
{
    public class HttpRequest : IUserService
    {
        public async Task<ExecutionResult> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .GetAsync($"http://localhost:5050/users?login={loginFromUser}&password={passwordFromUser}");
            
                if (response.IsSuccessStatusCode)
                    return ExecutionResult.CreateSuccessful();

                var description = await response.Content.ReadAsStringAsync();

                return ExecutionResult.CreateFailed(description);
            }
        }
    }
}