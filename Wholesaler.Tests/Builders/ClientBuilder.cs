using Wholesaler.Backend.DataAccess.Models;

namespace Wholesaler.Tests.Builders
{
    public class ClientBuilder
    {
        private Guid _id;
        private string _name;
        private string _surname;

        public ClientBuilder()
        {
            Refresh();
        }

        public void Refresh()
        {
            _id = Guid.NewGuid();
            _name = $"{Guid.NewGuid()}";
            _surname = $"{Guid.NewGuid()}";
        }

        public Client Build()
        {
            var client = new Client()
            {
                Id = _id,
                Name = _name,
                Surname = _surname
            };

            Refresh();

            return client;
        }
    }
}
