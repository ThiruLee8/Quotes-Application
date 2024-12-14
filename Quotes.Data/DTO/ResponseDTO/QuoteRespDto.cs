using Quotes.Data.DTO.RequestDTO;

namespace Quotes.Data.DTO.ResponseDTO
{
    public class QuoteRespDto : QuoteReqDto
    {
        public int QuoteId { get; set; }
    }
}
