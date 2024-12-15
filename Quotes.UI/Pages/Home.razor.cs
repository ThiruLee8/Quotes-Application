using Microsoft.AspNetCore.Components;
using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.Common.Enums;
using Quotes.UI.Components;
using Quotes.UI.Service.Dto.ApiRequest;
using Quotes.UI.Service.Dto.ApiResponse;

namespace Quotes.UI.Pages
{
    public partial class Home
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IDialogService DialogService { get; set; }

        private IEnumerable<Quote> pagedData;
        private MudTable<Quote> table;

        private int totalItems;
        private string searchString = null;

        private QuoteFilter Filter { get; set; } = new();

        private async Task<TableData<Quote>> ServerReload(TableState state, CancellationToken token)
        {
            try
            {
                Enum.TryParse(state.SortLabel, out QuoteColumnEnum sortColumn);

                Filter.SortColumn = sortColumn;
                Filter.IsAscending = state.SortDirection == SortDirection.Ascending ? true : false;
                Filter.PageSize = state.PageSize;
                Filter.CurrentPage = state.Page;

                List<Quote> data = await _quoteService.GetPaginatedQuotes(Filter);

                totalItems = data.FirstOrDefault()?.TotalCount ?? 0;
                return new TableData<Quote>() { TotalItems = totalItems, Items = data };
            }
            catch (Exception ex)
            {
                if (ex is UserFriendlyException)
                    snackBar.Add(ex.Message, Severity.Warning);
                else
                    snackBar.Add(ex.Message, Severity.Error);
                return new TableData<Quote>();
            }
        }

        private void RowClickEvent(TableRowClickEventArgs<Quote> tableRowClickEventArgs)
        {
            NavigationManager.NavigateTo($"/ViewQuote/{tableRowClickEventArgs.Item.QuoteId}");
        }
        private void OnQuoteEditClick(int quoteId)
        {
            NavigationManager.NavigateTo($"/UpdateQuote/{quoteId}");
        }
        private async Task OnQuoteDeletClick(int quoteId)
        {
            try
            {
                var options = new DialogOptions()
                {
                    CloseOnEscapeKey = true
                };
                var parameters = new DialogParameters<AppDialogComponent>
                {
                    { x => x.Title, "Delete Quote" },
                    {x =>x.Content, "Do you really want to delete Quote? This process cannot be undone." }
                };
                var dialog = DialogService.Show<AppDialogComponent>("Alert!", parameters, options);
                var result = await dialog.Result;
                if (result.Canceled || !bool.TryParse(result.Data.ToString(), out bool resultbool))
                    return;
                var resp = await _quoteService.DeleteQuote(quoteId);
                snackBar.Add(resp, Severity.Success);
                await table.ReloadServerData();
            }
            catch (Exception ex)
            {
                if (ex is UserFriendlyException)
                    snackBar.Add(ex.Message, Severity.Warning);
                else
                    snackBar.Add(ex.Message, Severity.Error);
            }
        }

        public void OnClickCreateQuotes()
        {
            NavigationManager.NavigateTo("/CreateQuotes");
        }

        public async Task OpenFilter()
        {
            try
            {
                var options = new DialogOptions()
                {
                    CloseOnEscapeKey = true
                };
                var parameters = new DialogParameters<FilterDialogComponent>
                {
                    { x => x.Filter, Filter  },
                };
                var oldFilter = Filter.DeepCopy();
                var dialog = DialogService.Show<FilterDialogComponent>("Filter!", parameters, options);
                var result = await dialog.Result;
                if (result.Canceled)
                {
                    Filter = oldFilter;
                    return;
                }
                Filter = (QuoteFilter)result.Data;
                await table.ReloadServerData();
                snackBar.Add("Filter Applied", Severity.Success);

            }
            catch (Exception ex)
            {
                if (ex is UserFriendlyException)
                    snackBar.Add(ex.Message, Severity.Warning);
                else
                    snackBar.Add(ex.Message, Severity.Error);
            }
        }

    }
}
