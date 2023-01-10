using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesaler.Frontend.DataAccess.Http
{
    public class Request<TRequestModel, TResponseModel>
    {
        public string Path { get; set; }
        public TRequestModel Content { get; set; }
        public HttpMethod Method { get; set; }
    }    
}
