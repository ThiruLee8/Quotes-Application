using Microsoft.AspNetCore.Components;

namespace Quotes.UI.Pages
{
    public partial class QuoteViewPage
    {
        [Parameter]
        public string QuoteId { get; set; }
    }
}
