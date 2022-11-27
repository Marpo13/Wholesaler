using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Backend.Domain.Entities
{
    public class WorkTask
    {
        public Guid Id { get; set; }
        public int PersonId { get; set; }
    }
}
