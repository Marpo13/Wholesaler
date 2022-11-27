using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Backend.DataAccess.Models
{
    public class WorkTask
    {
        public Guid Id { get; set; }
        public int NumberOfRows { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public virtual Requirement Requirement { get; set; }
        public int ReguirementId { get; set; }

        public WorkTask(int numberOfRows)
        {
            Id = Guid.NewGuid();
            NumberOfRows = numberOfRows;
        }
        public WorkTask()
        { }
    }
}
