using Quotes.Data.DTO.RequestDTO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quotes.Data.DTO.ResponseDTO
{
    public class QuoteRespDto : QuoteReqDto
    {
        public string QuoteStage { get; set; }
        public int QuoteId { get; set; }
    }
}
