using Microsoft.AspNetCore.Http;

namespace ElectronicJournal.Web.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest httpRequest)
            => httpRequest.QueryString.HasValue ? $"{httpRequest.Path}{httpRequest.QueryString}" : httpRequest.Path.ToString();
    }
}
