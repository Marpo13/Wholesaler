using Wholesaler.Core.Dto.ResponseModels;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.Views.Generic;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews.Components
{
    internal class SelectUserComponent : Component<UserDto>
    {
        private readonly List<UserDto> _users;

        public SelectUserComponent(List<UserDto> users)
        {
            _users = users;
        }

        public override UserDto Render()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Employees ID:");

            foreach (var employee in _users)
                Console.WriteLine($"{_users.IndexOf(employee) + 1}: {employee.Id}");
            Console.WriteLine("----------------------------");
            Console.WriteLine("Enter an id of an employee you want to choose: ");
            if (!int.TryParse(Console.ReadLine(), out int userNumber))
                throw new InvalidDataProvidedException("You entered an invalid value.");

            var index = userNumber - 1;
            var user = _users
                .Where(x => _users.IndexOf(x) == index)
                .FirstOrDefault();

            if (user == null)
                throw new InvalidDataProvidedException("You input an invalid value.");

            return user;
        }
    }
}
