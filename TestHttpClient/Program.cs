using Microsoft.Extensions.Options;
using Refit;
using TestHttpClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<GithubSettingsSetup>();

builder.Services.AddTransient<GithubAuthenticationHandler>();

builder.Services.AddRefitClient<IGithubApi>()
    .ConfigureHttpClient((sp, httpClient) =>
    {
        var githubSettings = sp.GetRequiredService<IOptions<GithubSettings>>().Value;
        httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    })
    .AddHttpMessageHandler<GithubAuthenticationHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndPoints();

app.Run();
