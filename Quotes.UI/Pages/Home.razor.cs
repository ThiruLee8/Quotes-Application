using MudBlazor;
using Quotes.Common.CustomExceptions;
using Quotes.UI.Service.Dto.ApiRequest;
using Quotes.UI.Service.Dto.ApiResponse;

namespace Quotes.UI.Pages
{
    public partial class Home
    {


        private IEnumerable<Quote> pagedData;
        private MudTable<Quote> table;

        private int totalItems;
        private string searchString = null;

        private async Task<TableData<Quote>> ServerReload(TableState state, CancellationToken token)
        {
            try
            {
                var filter = new QuoteFilter()
                {
                    PageSize = state.PageSize,
                    CurrentPage = state.Page,
                };

                List<Quote> data = await _quoteService.GetPaginatedQuotes(filter);

                totalItems = data.FirstOrDefault()?.TotalCount ?? 0;

                return new TableData<Quote>() { TotalItems = totalItems, Items = data };
            }
            catch (UserFriendlyException ex)
            {
                snackBar.Add(ex.Message, Severity.Warning);
                return new TableData<Quote>();
            }
            catch (Exception ex)
            {
                snackBar.Add(ex.Message, Severity.Error);
                return new TableData<Quote>();
            }
        }

        private void OnSearch(string text)
        {
            searchString = text;
            table.ReloadServerData();
        }
    }
}
