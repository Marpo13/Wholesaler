
using Wholesaler.Frontend.DataAccess;

namespace Wholesaler.Frontend.Domain
{
    public class Login
    {
        public async Task LoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            var httpRequest = new HttpRequest();
            await httpRequest.TryLoginWithDataFromUserAsync(loginFromUser, passwordFromUser);
        }
    }
}