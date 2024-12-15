using Newtonsoft.Json;
using Quotes.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.UI.Service.Dto.ApiRequest
{
    public class QuoteFilter
    {
        public string? AuthorFilter { get; set; }
        public List<string> TagsFilter { get; set; } = new();
        [JsonProperty("quoteFilter")]
        public string? InspirationalQuoteFilter { get; set; }
        public QuoteColumnEnum SortColumn { get; set; } = QuoteColumnEnum.QuoteId;
        public bool IsAscending { get; set; } = true;
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 10;

        public QuoteFilter(string? authorFilter, List<string> tagsFilter, string? inspirationalQuoteFilter, QuoteColumnEnum sortColumn=QuoteColumnEnum.QuoteId, bool isAscending=true, int currentPage=0, int pageSize=10)
        {
            AuthorFilter = authorFilter;
            TagsFilter = tagsFilter;
            InspirationalQuoteFilter = inspirationalQuoteFilter;
            SortColumn = sortColumn;
            IsAscending = isAscending;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
        public QuoteFilter()
        {
            
        }
        public QuoteFilter DeepCopy()
        {
            return new QuoteFilter(AuthorFilter, new List<string>(TagsFilter), InspirationalQuoteFilter, SortColumn,
                IsAscending, CurrentPage, PageSize);
        }
    }
}
