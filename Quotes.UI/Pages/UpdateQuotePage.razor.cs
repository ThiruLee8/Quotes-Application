using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.UI.Service.Dto.ApiResponse;

namespace Quotes.UI.Pages
{
    public partial class UpdateQuotePage
    {
        [Parameter]
        public string QuoteId { get; set; }
        
    }
}
