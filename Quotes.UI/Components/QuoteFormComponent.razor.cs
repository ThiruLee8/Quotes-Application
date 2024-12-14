using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.UI.Service.Dto.ApiRequest;

namespace Quotes.UI.Components
{
    public partial class QuoteFormComponent
    {

        MudForm form;

        [Parameter]
        public bool IsCreatePage { get; set; } = default;
        [Parameter]
        public bool IsUpdatePage { get; set; } = default;
        [Parameter]
        public int QuoteId {  get; set; }

        [Parameter]
        public QuoteReqDto Data { get; set; }
        public List<QuoteReqDto> ModelData { get; set; } = new();
        protected override void OnInitialized()
        {
            if (IsCreatePage)
                ModelData.Add(new());

        }

        private async void OnFormSubmit()
        {
            try
            {
                form.Validate();
                if (!form.IsValid)
                    return;
                if (IsCreatePage)
                {
                    var resp = await _quoteService.CreateQuote(ModelData);
                    ModelData.Clear();
                    ModelData.Add(new());
                    snackBar.Add(resp, Severity.Success);
                }
                else if (IsUpdatePage)
                {
                    var updateQuote = ModelData.FirstOrDefault();
                    var resp = await _quoteService.UpdateQuote(QuoteId, updateQuote);
                    snackBar.Add("Quote Updated Successfully.", Severity.Success);
                }
                StateHasChanged();
            }
            catch(UserFriendlyException ex)
            {
                snackBar.Add(ex.Message,Severity.Warning);
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message,Severity.Error);
            }
        }

        private void AddQuote()
        {
            if (IsCreatePage)
            {
                ModelData.Add(new QuoteReqDto());
                StateHasChanged();
            }

        }
    }
}
