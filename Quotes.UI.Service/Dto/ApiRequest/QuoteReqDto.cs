using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quotes.UI.Service.Dto.ApiRequest
{
    public class QuoteReqDto
    {
        [Required]
        public string Author { get; set; } = null!;
        public List<string> Tags { get; set; } = new();
        [Required]
        [JsonProperty("quote")]
        public string InspirationalQuote { get; set; } = null!;
    }
}
