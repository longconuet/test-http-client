using Microsoft.Extensions.Options;

namespace TestHttpClient
{
    public static class UsersEndpoints
    {
        public static void MapUserEndPoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id}", async (
                int id,
                IGithubApi githubApi) =>
            {
                var content = await githubApi.GetByUserId(id);

                return Results.Ok(content);
            });
        }
    }
}
