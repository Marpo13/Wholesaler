using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Core.Dto.RequestModels
{
    public class LoginUserRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
