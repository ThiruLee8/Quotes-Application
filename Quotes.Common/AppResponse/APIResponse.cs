using Quotes.Common.CustomExceptions;
using System.Net;

namespace Quotes.Common.AppResponse
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMsg { get; set; }
        public string StatusMsg { get; set; } = null!;
        public string? Response { get; set; }
        public string Timestamp { get; set; } = DateTime.Now.ToString();

        public void ValidateResponse()
        {
            if (StatusCode == HttpStatusCode.InternalServerError)
                throw new Exception(ErrorMsg);
            else if (StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(ErrorMsg);
        }
    }
}

