using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;

namespace Quotes.Data.Repositories.Interface
{
    public interface IQuoteRepo
    {
        Task<bool> CreateQuotesAsync(List<Quote> quotes);
        Task<List<Quote>> GetAllQuotesAsync();
        Task<Quote> GetQuoteByIdAsync(int id);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task<List<QuotePaginatedRespDto>> SearchQuoteAsync(QuoteFilter quote, CancellationToken cancellationToken);
    }
}
