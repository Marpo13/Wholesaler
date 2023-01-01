using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Backend.Domain.Entities
{
    public class Workday
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Stop { get; set; }
        public Person Person {get; set;}

        public Workday(DateTime start, Person person)
        {
            Id = Guid.NewGuid();
            Start = start;
            Stop = null;
            Person = person;
        }
    }
}
