using Microsoft.EntityFrameworkCore;
using Quotes.Common.Enums;
using Quotes.Data.DatabaseContext;
using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using Quotes.Data.Repositories.Interface;

namespace Quotes.Data.Repositories.Implementation
{
    public class QuoteRepo : IQuoteRepo
    {
        private readonly QuoteDbContext _context;

        public QuoteRepo(QuoteDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateQuotesAsync(List<Quote> quotes)
        {
            await _context.Quotes.AddRangeAsync(quotes);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<Quote>> GetAllQuotesAsync()
        {
            return await _context.Quotes.Where(x => !x.IsDeleted).Include(x => x.Tags).ToListAsync();
        }

        public async Task<Quote> GetQuoteByIdAsync(int id)
        {
            return await _context.Quotes.Where(x => x.QuoteId == id && !x.IsDeleted).Include(x => x.Tags).FirstOrDefaultAsync();
        }

        public async Task<List<QuotePaginatedRespDto>> SearchQuoteAsync(QuoteFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Quotes.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.AuthorFilter))
                query = query.Where(x => x.Author.ToLower().Contains(filter.AuthorFilter.ToLower()));
            if (filter.TagsFilter.Count > 0)
                query = query.Where(x => x.Tags.Any(z => filter.TagsFilter.Any(y => y == z.TagName)));
            if (!string.IsNullOrWhiteSpace(filter.InspirationalQuoteFilter))
                query = query.Where(x => x.InspirationalQuote.ToLower().Contains(filter.InspirationalQuoteFilter.ToLower()));


            switch (filter.SortColumn)
            {
                case QuoteColumnEnum.QuoteId:
                    query = filter.IsAscending
                ? query.OrderBy(x => x.QuoteId)
                : query.OrderByDescending(x => x.QuoteId);
                    break;
                case QuoteColumnEnum.Author:
                    query = filter.IsAscending
                ? query.OrderBy(x => x.Author.ToLower())
                : query.OrderByDescending(x => x.Author.ToLower());
                    break;
                case QuoteColumnEnum.Quote:
                    query = filter.IsAscending
                ? query.OrderBy(x => x.InspirationalQuote.ToLower())
                : query.OrderByDescending(x => x.InspirationalQuote.ToLower());
                    break;
                default:
                    query = query.OrderBy(x => x.QuoteId);
                    break;
            }
            var sf = query.ToQueryString();
            var total = await query.CountAsync();
            var finalRes = await query.Skip(filter.CurrentPage * filter.PageSize).Take(filter.PageSize)
                .Select(x => new QuotePaginatedRespDto
                {
                    Author = x.Author,
                    QuoteId = x.QuoteId,
                    InspirationalQuote = x.InspirationalQuote,
                    Tags = x.Tags.Select(x => x.TagName).ToList(),
                    TotalCount = total,
                }).ToListAsync();
            for (int i = 0; i < finalRes.Count; i++)
            {
                finalRes[i].RowNumber = i + (filter.CurrentPage * filter.PageSize) + 1;
            }
            return finalRes;
        }

        public async Task<Quote> UpdateQuoteAsync(Quote quote)
        {
            _context.Quotes.Update(quote);
            await _context.SaveChangesAsync();
            return quote;
        }
    }
}
