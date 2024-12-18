using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Data.EntityModals
{
    public class QuoteStage
    {
        [Key]
        public int QuoteStageId { get; set; }
        public string QuoteStageName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
