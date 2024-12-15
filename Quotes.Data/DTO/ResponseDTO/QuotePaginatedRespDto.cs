using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Data.DTO.ResponseDTO
{
    public class QuotePaginatedRespDto : QuoteRespDto
    {
        public int TotalCount { get; set; }
        public int RowNumber { get; set; }

    }
}
