using Microsoft.Extensions.Options;

namespace TestHttpClient
{
    public static class UsersEndpoints
    {
        public static void MapUserEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users/v4/{id}", async (
                int id,
                GithubService githubService) =>
            {
                var content = await githubService.GetByUserId(id);

                return Results.Ok(content);
            });

            app.MapGet("/users/v3/{id}", async (
                int id,
                IHttpClientFactory httpClientFactory) =>
            {
                var client = httpClientFactory.CreateClient("github");

                var content = await client.GetFromJsonAsync<GithubUser>($"users/{id}");

                return Results.Ok(content);
            });

            app.MapGet("/users/v2/{id}", async (
                int id, 
                IHttpClientFactory httpClientFactory,
                IOptions<GithubSettings> settings) =>
            {
                var client = httpClientFactory.CreateClient();

                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.DefaultRequestHeaders.Add("Accept", settings.Value.Accept);

                var content = await client.GetFromJsonAsync<GithubUser>($"users/{id}");

                return Results.Ok(content);
            });

            app.MapGet("/users/v1/{id}", async (int id, IOptions<GithubSettings> settings) =>
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                client.DefaultRequestHeaders.Add("Accept", settings.Value.Accept);

                var content = await client.GetFromJsonAsync<GithubUser>($"users/{id}");

                return Results.Ok(content);
            });
        }
    }
}
