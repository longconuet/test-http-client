using Microsoft.Extensions.Options;
using TestHttpClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<ApplicationOptions>(
//    builder.Configuration.GetSection(nameof(ApplicationOptions)));

builder.Services.ConfigureOptions<GithubSettingsSetup>();

builder.Services.AddHttpClient("github", (sp, httpClient) =>
{
    var githubSettings = sp.GetRequiredService<IOptions<GithubSettings>>().Value;
    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    httpClient.DefaultRequestHeaders.Add("Accept", githubSettings.Accept);
});

builder.Services.AddHttpClient<GithubService>((sp, httpClient) =>
{
    var githubSettings = sp.GetRequiredService<IOptions<GithubSettings>>().Value;
    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    httpClient.DefaultRequestHeaders.Add("Accept", githubSettings.Accept);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new SocketsHttpHandler()
    {
        PooledConnectionLifetime = TimeSpan.FromMinutes(15)
    };
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndPoints();

app.Run();
