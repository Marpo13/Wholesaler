using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Backend.DataAccess.Models
{
    public class Requirement
    {
        public Guid Id { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }
    }
}
