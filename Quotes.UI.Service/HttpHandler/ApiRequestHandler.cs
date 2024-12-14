using System.Net;
using System.Text;
using System.Web;
using Quotes.Common.AppResponse;

namespace Quotes.UI.Service.HttpHandler
{
    public class ApiRequestHandler
    {
        public async Task<APIResponse> CallApiAsync(string url, HttpMethod method, string? body = default, HttpContent? multiPartFormData = default, Dictionary<string, string>? header = default, Dictionary<string, string>? queryParams = null, bool IsSoapRequest = false)
        {
            APIResponse apiresponse = new APIResponse();

            //ssl certificate issue
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            //using HttpClient _client = new HttpClient(httpClientHandler);
            using HttpClient _client = new HttpClient();
            //using HttpClient _client = new HttpClient();
            _client.Timeout = TimeSpan.FromMinutes(40);

            var request = new HttpRequestMessage
            {
                Method = method
            };
            if (queryParams != null && queryParams.Count > 0)
            {
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var kvp in queryParams)
                {
                    query[kvp.Key] = kvp.Value;
                }
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }
            else
                request.RequestUri = new Uri(url);

            if (header != null)
            {
                foreach (var kvp in header)
                {
                    request.Headers.Add(kvp.Key, kvp.Value);
                }
            }

            if (method != HttpMethod.Get && !string.IsNullOrEmpty(body) && !IsSoapRequest)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            if (method != HttpMethod.Get && !string.IsNullOrEmpty(body) && IsSoapRequest)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/soap+xml");
            }

            if (method != HttpMethod.Get && multiPartFormData != null && !IsSoapRequest)
            {
                request.Content = multiPartFormData;

            }

            var response = await _client.SendAsync(request);

            apiresponse.StatusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                apiresponse.StatusMsg = ApiStatus.Success.ToString();
                // Read the content as a string
                string responseContent = await response.Content.ReadAsStringAsync();
                apiresponse.Response = responseContent;
                return apiresponse;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(HttpStatusCode.Unauthorized.ToString());
            else
            {
                apiresponse.StatusMsg = ApiStatus.Failure.ToString();
                if (response?.Content != null)
                {
                    apiresponse.ErrorMsg = response.ReasonPhrase;
                    apiresponse.Response = await response.Content.ReadAsStringAsync();
                    return apiresponse;
                }
                apiresponse.ErrorMsg = "something went wrong in API call";
                return apiresponse;
            }

        }


    }
}
