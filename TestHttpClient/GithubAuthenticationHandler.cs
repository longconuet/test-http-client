using Microsoft.Extensions.Options;

namespace TestHttpClient
{
    public class GithubAuthenticationHandler : DelegatingHandler
    {
        private readonly GithubSettings _githubSettings;

        public GithubAuthenticationHandler(IOptions<GithubSettings> options)
        {
            _githubSettings = options.Value;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Accept", _githubSettings.Accept);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
