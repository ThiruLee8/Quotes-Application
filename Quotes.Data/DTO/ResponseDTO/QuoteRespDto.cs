using Quotes.Data.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Data.DTO.ResponseDTO
{
    public class QuoteRespDto : QuoteReqDto
    {
        public int QuoteId {  get; set; }
    }
}
