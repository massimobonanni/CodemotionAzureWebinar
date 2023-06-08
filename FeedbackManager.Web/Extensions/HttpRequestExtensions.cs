using Azure.Core;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpRequestExtensions
    {
        private const string AADPrincipalNameHeader = "X-MS-CLIENT-PRINCIPAL-NAME";
        private const string PrincipalIDPHeader = "X-MS-CLIENT-PRINCIPAL-IDP";
        private const string PrincipalTokenHeader = "X-MS-CLIENT-PRINCIPAL";
        private const string GitHubAccessTokenHeader = "X-MS-TOKEN-GITHUB-ACCESS-TOKEN";
        private const string AADAccessTokenHeader = "X-MS-TOKEN-AAD-ACCESS-TOKEN";

        public static string? GetAADPrincipalName(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(AADPrincipalNameHeader))
                return request.Headers[AADPrincipalNameHeader][0];
            return null;
        }

        public static string? GetIdentityProvider(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(PrincipalIDPHeader))
                return request.Headers[PrincipalIDPHeader][0];
            return null;
        }

        public static string? GetToken(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(PrincipalTokenHeader))
                return request.Headers[PrincipalTokenHeader][0];
            return null;
        }

        public static string? GetGitHubAccessToken(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(GitHubAccessTokenHeader))
                return request.Headers[GitHubAccessTokenHeader][0];
            return null;
        }

        private static string? GetAADAccessToken(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(AADAccessTokenHeader))
                return request.Headers[AADAccessTokenHeader][0];
            return null;
        }
    }
}
