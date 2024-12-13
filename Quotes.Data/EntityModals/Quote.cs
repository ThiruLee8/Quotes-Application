using System.ComponentModel.DataAnnotations;

namespace Quotes.Data.EntityModals
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public string Author { get; set; } = null!;
        public string InspirationalQuote { get; set; } = null!;
        public bool IsDeleted {  get; set; } = false;
        public virtual List<Tag> Tags { get; set; } = new();

    }
}
