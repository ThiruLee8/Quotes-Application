using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Common.DTO.ResponseDTO
{
    public class QuotePaginatedRespDto : QuoteRespDto
    {
        public int TotalCount { get; set; }

    }
}
