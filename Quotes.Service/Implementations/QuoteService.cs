using AutoMapper;
using Quotes.Common.AppResponse;
using Quotes.Common.Constants;
using Quotes.Common.CustomExceptions;
using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using Quotes.Data.Repositories.Interface;
using Quotes.Service.Interfaces;

namespace Quotes.Service.Implementations
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepo _quoteRepo;
        private readonly IMapper _mapper;

        public QuoteService(IQuoteRepo quoteRepo, IMapper mapper)
        {
            _quoteRepo = quoteRepo;
            _mapper = mapper;
        }
        public async Task<GenericResponse<string>> CreateQuotes(List<QuoteReqDto> req)
        {
            List<Quote> quotes = _mapper.Map<List<Quote>>(req);
            var resp = await _quoteRepo.CreateQuotesAsync(quotes);

            return AppResponseFactory.SuccessResponse(AppMessage.QuoteCreatedSuccess);
        }

        public async Task<GenericResponse<string>> DeleteQuote(int id)
        {
            var quote = await _quoteRepo.GetQuoteByIdAsync(id);
            if (quote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);

            quote.IsDeleted = true;
            await _quoteRepo.UpdateQuoteAsync(quote);
            return AppResponseFactory.SuccessResponse(AppMessage.DeleteQuote);
        }

        public async Task<GenericResponse<List<QuoteRespDto>>> GetAllQuotes()
        {
            var quotes = await _quoteRepo.GetAllQuotesAsync();
            var resp = _mapper.Map<List<QuoteRespDto>>(quotes);
            return AppResponseFactory.SuccessResponse(resp);
        }

        public async Task<GenericResponse<QuoteRespDto>> GetQuoteById(int id)
        {
            var quote = await _quoteRepo.GetQuoteByIdAsync(id);
            if (quote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);
            var resp = _mapper.Map<QuoteRespDto>(quote);
            return AppResponseFactory.SuccessResponse(resp);
        }

        public async Task<GenericResponse<List<QuotePaginatedRespDto>>> SearchQuote(QuoteFilter req, CancellationToken cancellationToken)
        {
            var quotes = await _quoteRepo.SearchQuoteAsync(req, cancellationToken);
            return AppResponseFactory.SuccessResponse(quotes);
        }

        public async Task<GenericResponse<QuoteRespDto>> UpdateQuote(int id, QuoteReqDto req, string userRole)
        {
            List<string> usersList = ["user", "validator", "admin"];
            if (!usersList.Contains(userRole))
                throw new ForbiddenAppException(AppMessage.Forbidden);
            var quote = await _quoteRepo.GetQuoteByIdAsync(id);
            if (quote == null)
                throw new UserFriendlyException(AppMessage.InvalidQuoteId);

            var quoteReq = _mapper.Map<Quote>(req);
            if (userRole == "User")
            {
                quote.Author = quoteReq.Author;
                quote.Tags = quoteReq.Tags;
                quote.InspirationalQuote = quoteReq.InspirationalQuote;
            }
            else if(userRole == "Validator" && (quoteReq.QuoteStageId == 2 || quoteReq.QuoteStageId == 3))
            {
                quote.QuoteStageId = quoteReq.QuoteStageId;
            }
            else if(userRole == "Admin" && (quoteReq.QuoteStageId == 4 || quoteReq.QuoteStageId == 5))
            {
                quote.QuoteStageId = quoteReq.QuoteStageId;
            }
            else
            {
                throw new ForbiddenAppException(AppMessage.Forbidden);
            }

            var updatedQuote = await _quoteRepo.UpdateQuoteAsync(quote);

            var resp = _mapper.Map<QuoteRespDto>(updatedQuote);
            return AppResponseFactory.SuccessResponse(resp);
        }
    }
}
