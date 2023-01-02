using Newtonsoft.Json;
using System.Net.Http.Json;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;
using static Wholesaler.Frontend.Domain.ValueObjects.ExecutionResultGeneric;

namespace Wholesaler.Frontend.DataAccess
{
    public class WholesalerClient : IUserService
    {
        private const string apiPath = $"http://localhost:5050";

        public async Task<ExecutionResult<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            
            using (var httpClient = new HttpClient())
            {
                using (var postRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{apiPath}/users/actions/login"))
                {
                    var content = new LoginUserRequestModel
                    {
                        Login = loginFromUser,
                        Password = passwordFromUser,
                    };

                    postRequestMessage.Content = JsonContent.Create(content);

                    var postResult = await httpClient.SendAsync(postRequestMessage);

                    if (postResult.IsSuccessStatusCode)
                    {
                        var person = JsonConvert.DeserializeObject<UserDto>(await postResult.Content.ReadAsStringAsync());
                        return ExecutionResult<UserDto>.CreateSuccessful(person);
                    }                                         

                    return ExecutionResult<UserDto>.CreateFailed("Error.");
                }                
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