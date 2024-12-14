using Quotes.Common.AppResponse;
using Quotes.Common.DTO.RequestDTO;
using Quotes.Common.DTO.ResponseDTO;

namespace Quotes.Service.Interfaces
{
    public interface IQuoteService
    {
        Task<GenericResponse<string>> CreateQuotes(List<QuoteReqDto> req);
        Task<GenericResponse<List<QuoteRespDto>>> GetAllQuotes();
        Task<GenericResponse<QuoteRespDto>> GetQuoteById(int id);
        Task<GenericResponse<QuoteRespDto>> UpdateQuote(int id, QuoteReqDto req);
        Task<GenericResponse<string>> DeleteQuote(int id);
        Task<GenericResponse<List<QuotePaginatedRespDto>>> SearchQuote(QuoteFilter req);
    }
}
