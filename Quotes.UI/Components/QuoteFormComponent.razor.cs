using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.UI.Service.Dto.ApiRequest;
using Quotes.UI.Service.Dto.ApiResponse;

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
        public int QuoteId { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public List<QuoteRespDto> ModelData { get; set; } = new();
        protected async override Task OnInitializedAsync()
        {
            try
            {
                if (IsCreatePage)
                    ModelData.Add(new());
                else if (QuoteId != 0)
                {
                    var data = await _quoteService.GetQuoteById(QuoteId);
                    ModelData.Add(data);
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch (Exception ex)
            {
                if (ex is UserFriendlyException)
                    snackBar.Add(ex.Message, Severity.Warning);
                else
                    snackBar.Add(ex.Message, Severity.Error);
            }

        }

        //protected override void OnAfterRender(bool firstRender)
        //{
        //    if (Data != null)
        //    {
        //        ModelData.Add(Data);
        //    }

        //}

        private async void OnFormSubmit()
        {
            try
            {
                form.Validate();
                if (!form.IsValid)
                    return;
                var req = ModelData.Select(x => (QuoteReqDto)x).ToList();
                if (IsCreatePage)
                {
                    var resp = await _quoteService.CreateQuote(req);
                    ModelData.Clear();
                    ModelData.Add(new());
                    snackBar.Add(resp, Severity.Success);
                }
                else if (IsUpdatePage)
                {
                    var updateQuote = req.FirstOrDefault();
                    var resp = await _quoteService.UpdateQuote(QuoteId, updateQuote,AppState.UserRole);
                    snackBar.Add("Quote Updated Successfully.", Severity.Success);
                }
                NavigationManager.NavigateTo("/");
            }
            catch (UserFriendlyException ex)
            {
                snackBar.Add(ex.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }
        }

        public async Task UpdateStage(bool isApproved)
        {
            try
            {
                var req = ModelData.Select(x => (QuoteReqDto)x).ToList();
                if (AppState.UserRole == "validator")
                {
                    var updateQuote = req.FirstOrDefault();
                    if (isApproved)
                        updateQuote.QuoteStageId = 2;
                    else
                        updateQuote.QuoteStageId = 3;
                    var resp = await _quoteService.UpdateQuote(QuoteId, updateQuote,AppState.UserRole);
                    snackBar.Add("Quote Updated Successfully.", Severity.Success);
                }
                else if (AppState.UserRole == "admin")
                {
                    var updateQuote = req.FirstOrDefault();
                    if (isApproved)
                        updateQuote.QuoteStageId = 4;
                    else
                        updateQuote.QuoteStageId = 5;
                    var resp = await _quoteService.UpdateQuote(QuoteId, updateQuote,AppState.UserRole);
                    snackBar.Add("Quote Updated Successfully.", Severity.Success);
                }
                NavigationManager.NavigateTo("/");
            }
            catch (UserFriendlyException ex)
            {
                snackBar.Add(ex.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
            }
        }
        private void AddQuote()
        {
            if (IsCreatePage)
            {
                ModelData.Add(new QuoteRespDto());
                StateHasChanged();
            }

        }
    }
}
