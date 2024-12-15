using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;

namespace Quotes.UI.Components
{
    public partial class ChipComponent
    {
        [Parameter]
        public List<string> Tags { get; set; }
        [Parameter]
        public EventCallback Callback { get; set; }
        [Parameter]
        public bool IsClosable { get; set; }
        [Parameter]
        public int Index { get; set; } = 0;

        public async void Closed(MudChip<string> chip)
        {
            if (IsClosable && Callback.HasDelegate)
            {
                await Callback.InvokeAsync(chip.Value);
            }
        }
        Array colors = Enum.GetValues(typeof(Color));
        private Color GetRandomColor()
        {
            var color= (Color)(Index % colors.Length);
            Index++;
            return color;
        }
    }
}
