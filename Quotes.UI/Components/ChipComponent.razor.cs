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

        private Random _random = new Random();
        public async void Closed(MudChip<string> chip)
        {
            if (IsClosable && Callback.HasDelegate)
            {
                await Callback.InvokeAsync(chip.Value);
            }   
        }

        private Color GetRandomColor()
        {
            var colors = Enum.GetValues(typeof(Color));
            return (Color)colors.GetValue(_random.Next(colors.Length));
        }
    }
}
