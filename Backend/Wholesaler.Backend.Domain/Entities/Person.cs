namespace Wholesaler.Backend.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Password { get; }

        public Person(Guid id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }
    }
}