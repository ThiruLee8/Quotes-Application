using Quotes.Common.DTO.RequestDTO;
using Quotes.Common.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories.Interface
{
    public interface IQuoteRepo
    {
        Task<bool> CreateQuotesAsync(List<Quote> quotes);
        Task<List<Quote>> GetAllQuotesAsync();
        Task<Quote> GetQuoteByIdAsync(int id);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task<List<QuotePaginatedRespDto>> SearchQuoteAsync(QuoteFilter quote);
    }
}
