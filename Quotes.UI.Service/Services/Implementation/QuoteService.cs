using Quotes.UI.Service.Constants;
using Quotes.UI.Service.HttpHandler;
using Quotes.UI.Service.Services.Interface;
using System.Net;
using Quotes.Common.CustomExceptions;
using Quotes.Common.AppResponse;
using Newtonsoft.Json;
using Quotes.UI.Service.Dto.ApiResponse;
using Quotes.UI.Service.Dto.ApiRequest;

namespace Quotes.UI.Service.Services.Implementation
{
    public class QuoteService : IQuoteService
    {
        private readonly ApiRequestHandler _apiRequestHandler;

        public QuoteService(ApiRequestHandler apiRequestHandler)
        {
            _apiRequestHandler = apiRequestHandler;
        }

        public async Task<string> CreateQuote(List<QuoteReqDto> quotes)
        {
            var body = JsonConvert.SerializeObject(quotes);
            var resp = await _apiRequestHandler.CallApiAsync(AppUrl.CreateQuotes, HttpMethod.Post, body);

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(resp.ErrorMsg);

            var response = JsonConvert.DeserializeObject<GenericResponse<string>>(resp.Response);
            return response.Response;
        }

        public async Task<string> DeleteQuote(int quoteId)
        {
            string url = AppUrl.DeleteQuote.Replace(":id",quoteId.ToString());
            var resp = await _apiRequestHandler.CallApiAsync(url, HttpMethod.Delete);

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(resp.ErrorMsg);

            var response = JsonConvert.DeserializeObject<GenericResponse<string>>(resp.Response);
            return response.Response;
        }

        public async Task<List<Quote>> GetPaginatedQuotes(QuoteFilter filter)
        {

            var body = JsonConvert.SerializeObject(filter);
            var resp = await _apiRequestHandler.CallApiAsync(AppUrl.GetQuotesPaginated, HttpMethod.Post, body);

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(resp.ErrorMsg);

            var response = JsonConvert.DeserializeObject<GenericResponse<List<Quote>>>(resp.Response);
            return response.Response;
        }

        public async Task<QuoteRespDto> GetQuoteById(int quoteId)
        {
            var resp = await _apiRequestHandler.CallApiAsync(AppUrl.GetQuoteById.Replace(":id",quoteId.ToString()), HttpMethod.Get);

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(resp.ErrorMsg);

            var response = JsonConvert.DeserializeObject<GenericResponse<QuoteRespDto>>(resp.Response);
            return response.Response;
        }

        public async Task<QuoteRespDto> UpdateQuote(int quoteId, QuoteReqDto quote)
        {
            var body = JsonConvert.SerializeObject(quote);
            string url = AppUrl.UpdateQuote.Replace(":id",quoteId.ToString());
            var resp = await _apiRequestHandler.CallApiAsync(url, HttpMethod.Put, body);

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new UserFriendlyException(resp.ErrorMsg);

            var response = JsonConvert.DeserializeObject<GenericResponse<QuoteRespDto>>(resp.Response);
            return response.Response;
        }
    }
}
