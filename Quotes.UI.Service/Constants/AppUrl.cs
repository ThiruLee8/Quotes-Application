using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.UI.Service.Constants
{
    public class AppUrl
    {
        public const string BaseUrl = "http://localhost:5246";

        public const string CreateQuotes = $"{BaseUrl}/api/Quotes/CreateQuotes";
        public const string GetAllQuotes = $"{BaseUrl}/api/Quotes/GetAllQuotes";
        public const string GetQuoteById = $"{BaseUrl}/api/Quotes/GetQuote/:id";
        public const string UpdateQuote = $"{BaseUrl}/api/Quotes/UpdateQuote/:id";
        public const string DeleteQuote = $"{BaseUrl}/api/Quotes/DeleteQuote/:id";
        public const string GetQuotesPaginated = $"{BaseUrl}/api/Quotes/Search";
    }
}
