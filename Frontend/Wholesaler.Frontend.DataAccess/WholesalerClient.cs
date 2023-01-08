using Newtonsoft.Json;
using System.Net.Http.Json;
using Wholesaler.Core.Dto.RequestModels;
using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Domain;
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.DataAccess
{
    public class WholesalerClient : IUserService
    {
        private const string apiPath = $"http://localhost:5050";

        public async Task<ExecutionResultGeneric<UserDto>> TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
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
                    var postContent = await postResult.Content.ReadAsStringAsync();

                    if (postResult.IsSuccessStatusCode)
                    {
                        var person = JsonConvert.DeserializeObject<UserDto>(await postResult.Content.ReadAsStringAsync());
                        return ExecutionResultGeneric<UserDto>.CreateSuccessful(person);
                    }

                    return ExecutionResultGeneric<UserDto>.CreateFailed(postContent);
                }
            }
        }

        public async Task<ExecutionResultGeneric<Guid>> StartWorkingAsync(Guid userId)
        {
            using (var httpClient2 = new HttpClient())
            {
                using (var postRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{apiPath}/workdays/start"))
                {
                    var content = new StartWorkdayRequestModel
                    {
                        UserId = userId,
                    };

                    postRequestMessage.Content = JsonContent.Create(content);

                    var postResult = await httpClient2.SendAsync(postRequestMessage);
                    var postContent = await postResult.Content.ReadAsStringAsync();

                    if (postResult.IsSuccessStatusCode)
                    {
                        var workdayId = JsonConvert.DeserializeObject<Guid>(postContent);
                        return ExecutionResultGeneric<Guid>.CreateSuccessful(workdayId);
                    }

                    return ExecutionResultGeneric<Guid>.CreateFailed(postContent);
                }
            }
        }

        public async Task<ExecutionResultGeneric<WorkdayDto>> GetWorkdayAsync(Guid workdayid)
        {
            using (var httpClient4 = new HttpClient())
            {

                var result = await httpClient4.GetAsync($"{apiPath}/workdays/{workdayid}");
                var content = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    var workday = JsonConvert.DeserializeObject<WorkdayDto>(content);
                    return ExecutionResultGeneric<WorkdayDto>.CreateSuccessful(workday);
                }

                return ExecutionResultGeneric<WorkdayDto>.CreateFailed(content);

            }
        }
    }
}