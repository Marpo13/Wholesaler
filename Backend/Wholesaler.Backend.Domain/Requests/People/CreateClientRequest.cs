namespace Wholesaler.Backend.Domain.Requests.People;

public class CreateClientRequest
{
    public CreateClientRequest(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public string Name { get; }
    public string Surname { get; }
}
