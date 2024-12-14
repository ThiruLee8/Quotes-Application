using Quotes.Common.Enums;
using System.Text.Json.Serialization;

namespace Quotes.Data.DTO.RequestDTO
{
    public class QuoteFilter
    {
        public string? AuthorFilter { get; set; }
        public List<string> TagsFilter { get; set; } = new();
        [JsonPropertyName("quoteFilter")]
        public string? InspirationalQuoteFilter { get; set; }

        public QuoteColumnEnum SortColumn { get; set; } = QuoteColumnEnum.QuoteId;
        public bool IsAscending {  get; set; } = true;
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}
