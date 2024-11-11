using System.Net.Http;

namespace TestHttpClient
{
    public class GithubService
    {
        private readonly HttpClient _httpClient;

        public GithubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GithubUser?> GetByUserId(int id)
        {
            var content = await _httpClient.GetFromJsonAsync<GithubUser>($"users/{id}");
            return content;
        }
    }
}
