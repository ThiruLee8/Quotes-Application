using Quotes.UI.Service.Dto.ApiRequest;
using Quotes.UI.Service.Dto.ApiResponse;

namespace Quotes.UI.Service.Services.Interface
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetPaginatedQuotes(QuoteFilter filter);
        Task<string> CreateQuote(List<QuoteReqDto> quotes);
        Task<QuoteRespDto> UpdateQuote(int quoteId, QuoteReqDto quote);
        Task<QuoteRespDto> GetQuoteById(int quoteId);
        Task<string> DeleteQuote(int quoteId);
    }
}
