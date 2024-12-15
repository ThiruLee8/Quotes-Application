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
        //Array colors = Enum.GetValues(typeof(Color));

        List<Color> colors = [Color.Success,Color.Info,Color.Warning, Color.Error, Color.Dark, Color.Primary,
            Color.Secondary];
        private Color GetRandomColor()
        {
            var color= colors[(Index % colors.Count)];
            Index++;
            return color;
        }
    }
}
