using Azure.Core;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {
        private const string PrincipalNameHeader = "X-MS-CLIENT-PRINCIPAL-NAME";

        public static string? GetPrincipalName(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(PrincipalNameHeader))
                return request.Headers[PrincipalNameHeader][0];
            return null;
        }
    }
}
