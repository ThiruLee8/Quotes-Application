using System.ComponentModel.DataAnnotations;

namespace Quotes.Data.EntityModals
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual List<Quote> Quotes { get; set; }
        
    }
}
