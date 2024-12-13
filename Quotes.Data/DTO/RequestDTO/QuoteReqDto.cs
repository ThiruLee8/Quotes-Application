using Quotes.Data.EntityModals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quotes.Data.DTO.RequestDTO
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
