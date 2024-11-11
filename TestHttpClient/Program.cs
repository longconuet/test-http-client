using Microsoft.Extensions.Options;
using TestHttpClient;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<ApplicationOptions>(
//    builder.Configuration.GetSection(nameof(ApplicationOptions)));

builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

var app = builder.Build();

app.MapGet("/options", (
    IOptions<ApplicationOptions> options,
    IOptionsSnapshot<ApplicationOptions> optionsSnapshot,
    IOptionsMonitor<ApplicationOptions> optionsMonitor) =>
{
    var response = new
    {
        OptionsValue = options.Value.ExampleValue,
        OptionsSnapshotValue = optionsSnapshot.Value.ExampleValue,
        OptionsMonitorValue = optionsMonitor.CurrentValue.ExampleValue
    };

    return Results.Ok(response);
});

app.Run();