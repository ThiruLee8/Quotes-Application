using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.UI.Service.Dto.ApiRequest;
using Quotes.UI.Service.Dto.ApiResponse;

namespace Quotes.UI.Pages
{
    public partial class QuoteViewPage
    {
        [Parameter]
        public string QuoteId { get; set; }

        public QuoteReqDto Modaldata { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(QuoteId) && int.TryParse(QuoteId, out int id))
                {
                    Modaldata = await _quoteService.GetQuoteById(id);
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch(Exception ex) 
            {
                if (ex is UserFriendlyException)
                    snackBar.Add(ex.Message, Severity.Warning);
                else
                    snackBar.Add(ex.Message,Severity.Error);
            }
        }
    }
}
