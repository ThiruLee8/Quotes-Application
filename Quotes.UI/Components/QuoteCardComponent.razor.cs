using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.UI.Service.Dto.ApiRequest;

namespace Quotes.UI.Components
{
    public partial class QuoteCardComponent
    {
        bool success;
        string[] errors = { };
        MudForm form;


        [Parameter]
        public bool IsCreatePage {  get; set; }
        [Parameter]
        public bool IsUpdatePage { get; set; }
        public bool IsViewPage { get; set; } = true;

        [Parameter]
        public QuoteReqDto Data { get; set; }

        private string TagText { get; set; }

        protected override void OnInitialized()
        {
            if(IsCreatePage || IsUpdatePage)
                IsViewPage = false;
        }
        public void AddTag()
        {
            if (!string.IsNullOrWhiteSpace(TagText) && !Data.Tags.Contains(TagText) && !IsViewPage)
            {
                Data.Tags.Add(TagText.Trim());
                TagText = null;
                StateHasChanged();
            }
        }

        public void RemoveTag(object data)
        {
            var tag = data.ToString();
            if (!string.IsNullOrWhiteSpace(tag) && Data.Tags.Contains(tag) && !IsViewPage)
            {
                Data.Tags.Remove(tag);
                StateHasChanged();
            }
        }

    }
}
