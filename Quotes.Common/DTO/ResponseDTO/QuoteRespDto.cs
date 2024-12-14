using Quotes.Common.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Common.DTO.ResponseDTO
{
    public class QuoteRespDto : QuoteReqDto
    {
        public int QuoteId { get; set; }
    }
}
