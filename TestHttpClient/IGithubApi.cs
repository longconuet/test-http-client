using Refit;

namespace TestHttpClient
{
    public interface IGithubApi
    {
        [Get("/users/{userId}")]
        Task<GithubUser?> GetByUserId(int userId);
    }
}
