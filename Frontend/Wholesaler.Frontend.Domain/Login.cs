
using Wholesaler.Frontend.Domain.ValueObjects;

namespace Wholesaler.Frontend.Domain
{
    public class Login
    {
        private readonly IUserService _userService;

        public Login(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ExecutionResult> LoginWithDataFromUserAsync(string loginFromUser, string passwordFromUser)
        {
            return await _userService.TryLoginWithDataFromUserAsync(loginFromUser, passwordFromUser);
        }
    }
}