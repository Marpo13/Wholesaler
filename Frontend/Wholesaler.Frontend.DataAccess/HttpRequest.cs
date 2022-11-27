using static System.Net.WebRequestMethods;

namespace Wholesaler.Frontend.DataAccess
{
    public class HttpRequest
    {
        public async Task TryLoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .GetAsync($"http://localhost:5050/users?login={loginFromUser}&password={passwordFromUser}");
                if (response.IsSuccessStatusCode)
                    return;

                throw new Exception("You entered invalid login or password.");
            }
        }
    }
}