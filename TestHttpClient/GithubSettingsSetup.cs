using Microsoft.Extensions.Options;

namespace TestHttpClient
{
    public class GithubSettingsSetup : IConfigureOptions<GithubSettings>
    {
        private readonly IConfiguration _configuration;

        public GithubSettingsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(GithubSettings options)
        {
            _configuration.GetSection(nameof(GithubSettings))
                .Bind(options);
        }
    }
}
