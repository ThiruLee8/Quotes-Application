using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.Data.DTO.RequestDTO
{
    public class QuoteFilter
    {
        public string AuthorFilter {  get; set; }
        public List<string> TagsFilter { get; set; } = new();
        [JsonPropertyName("quoteFilter")]
        public string InspirationalQuoteFilter { get; set; }

        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 5;
    }
}
