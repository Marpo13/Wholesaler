using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Core.Dto.RequestModels
{
    public class AddPersonReqestModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }    
    }
}
