using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quotes.Common.DTO.RequestDTO
{
    public class QuoteReqDto
    {
        [Required]
        public string Author { get; set; } = null!;
        public List<string> Tags { get; set; } = new();
        [Required]
        [JsonPropertyName("quote")]
        public string InspirationalQuote { get; set; } = null!;
    }
}
