using Newtonsoft.Json;
using System.Net.Http.Json;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;
using static Wholesaler.Frontend.Domain.ValueObjects.ExecutionResultGeneric;

namespace Wholesaler.Frontend.DataAccess
{
    public class Repository : IUserService
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

        public async Task<ExecutionResult> StartWorking(Guid userId)
        {
            using (var httpClient2 = new HttpClient())
            {
                using (var postRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5050/workday"))
                {
                    var content = new StartWorkdayRequestModel
                    {
                        UserId = userId,
                    };                   

                    postRequestMessage.Content = JsonContent.Create(content);

                    var postResult = await httpClient2.SendAsync(postRequestMessage);

                    if (postResult.IsSuccessStatusCode)
                        return ExecutionResult.CreateSuccessful();

                    return ExecutionResult.CreateFailed("Error.");
                }
            }
        }
    }
}