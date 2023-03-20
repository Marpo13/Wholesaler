using Wholesaler.Backend.DataAccess.Models;
using Role = Wholesaler.Backend.Domain.Entities.Role;

namespace Wholesaler.Tests.Builders
{
    public class PersonBuilder
    {
        private Role _role;
        private Guid _id;
        private string _login;
        private string _password;
        private string _name;
        private string _surname;

        public PersonBuilder()
        {
            Refresh();
        }

        public void Refresh()
        {
            _id = Guid.NewGuid();
            _role = Role.Employee;
            _login = $"{Guid.NewGuid()}";
            _password = $"{Guid.NewGuid()}";
            _name = $"{Guid.NewGuid()}";
            _surname = $"{Guid.NewGuid()}";
        }

        public Person Build()
        {
            var person = new Person()
            {
                Id = _id,
                Role = _role,
                Login = _login,
                Name = _name,
                Password = _password,
                Surname = _surname
            };

            Refresh();

            return person;
        }

        public PersonBuilder WithRole(Role role)
        {
            _role = role;
            return this;
        }
    }
}
