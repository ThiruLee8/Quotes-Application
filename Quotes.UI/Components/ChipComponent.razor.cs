using Microsoft.AspNetCore.Components;

namespace Quotes.UI.Components
{
    public partial class ChipComponent
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public EventCallback Callback { get; set; }

        public async void Closed()
        {
            await Callback.InvokeAsync();
        }
    }
}
