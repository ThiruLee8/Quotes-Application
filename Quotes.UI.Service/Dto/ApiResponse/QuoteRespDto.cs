using Newtonsoft.Json;
using Quotes.UI.Service.Dto.ApiRequest;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quotes.UI.Service.Dto.ApiResponse
{
    public class QuoteRespDto : QuoteReqDto
    {
        public string QuoteStage { get; set; }
        public int QuoteId { get; set; }
    }
}
