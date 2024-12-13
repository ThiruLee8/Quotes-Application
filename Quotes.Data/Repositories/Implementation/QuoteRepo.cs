﻿using Microsoft.EntityFrameworkCore;
using Quotes.Data.DatabaseContext;
using Quotes.Data.DTO.RequestDTO;
using Quotes.Data.DTO.ResponseDTO;
using Quotes.Data.EntityModals;
using Quotes.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<QuotePaginatedRespDto>> SearchQuoteAsync(QuoteFilter quote)
        {
            var query = _context.Quotes.Include(x => x.Tags).Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrWhiteSpace(quote.AuthorFilter))
                query = query.Where(x => x.Author.ToLower().Contains(quote.AuthorFilter.ToLower()));
            if (quote.TagsFilter.Count > 0)
                query = query.Where(x => x.Tags.Any(z => quote.TagsFilter.Any(y => y == z.TagName)));
            if (!string.IsNullOrWhiteSpace(quote.InspirationalQuoteFilter))
                query = query.Where(x => x.InspirationalQuote.ToLower().Contains(quote.InspirationalQuoteFilter.ToLower()));

            var total = await query.CountAsync();
            var finalRes = query.Skip(quote.CurrentPage * quote.PageSize).Take(quote.PageSize)
                .Select(x => new QuotePaginatedRespDto
                {
                    Author = x.Author,
                    QuoteId = x.QuoteId,
                    InspirationalQuote = x.InspirationalQuote,
                    Tags = x.Tags.Select(x => x.TagName).ToList(),
                    TotalCount = total,
                }).ToList();
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