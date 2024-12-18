using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quotes.Data.EntityModals
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public string Author { get; set; } = null!;
        public string InspirationalQuote { get; set; } = null!;
        [ForeignKey(nameof(Stage))]
        public int QuoteStageId { get; set; } = 1;
        public bool IsDeleted {  get; set; } = false;
        public virtual List<Tag> Tags { get; set; } = new();
        public virtual QuoteStage? Stage { get; set; }


    }
}
