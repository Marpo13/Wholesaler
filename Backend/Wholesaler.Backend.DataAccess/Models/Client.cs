using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Backend.DataAccess.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Requirement> Requirements { get; set; }

        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Client()
        { }
    }
}
