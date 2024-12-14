using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.UI.Service.Dto.ApiResponse
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Author { get; set; } = null!;
        public List<string> Tags { get; set; } = new();
        [JsonProperty("quote")]
        public string InspirationalQuote { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
