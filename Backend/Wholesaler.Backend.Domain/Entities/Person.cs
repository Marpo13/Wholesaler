namespace Wholesaler.Backend.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }
        public Role Role { get; }
        public string Name { get; }
        public string Surname { get; }

        public Person(string login, string password, Role role, string name, string surname)
        {
            Login = login;
            Password = password;
            Role = role;
            Name = name;
            Surname = surname;
        }      
    }
}