namespace Wholesaler.Backend.Domain.Requests.People
{
    public class CreatePersonRequest
    {
        public string Name { get; }
        public string Surname { get; }
        public string Role { get; }
        public string Login { get; }
        public string Password { get; }

        public CreatePersonRequest(string name, string surname, string role, string login, string password)
        {
            Name = name;
            Surname = surname;
            Role = role;
            Login = login;
            Password = password;
        }
    }
}
