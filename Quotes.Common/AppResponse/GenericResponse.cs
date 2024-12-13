using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Common.AppResponse
{
    public class GenericResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Status {  get; set; }
        public List<string> Errors { get; set; } = new();
        public T? Response { get; set; }

    }
}
