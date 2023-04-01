using Wholesaler.Backend.Domain.Entities;
using Wholesaler.Core.Dto.ResponseModels;

namespace Wholesaler.Backend.Api.Factories
{
    public class UserDtoFactory : IUserDtoFactory
    {
        public UserDto Create(Person person)
        {
            var userDto = new UserDto()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Login = person.Login,
                Role = person.Role.ToString()
            };

            return userDto;
        }
    }
}
