using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Power655.Scraper;

public static class ServiceCollectionExtensions
{
    public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko)";

    /// <summary>
    /// Đăng ký VietlottSource, MinhNgocSource (HttpClient qua IHttpClientFactory) và DrawFetchService.
    /// Mỗi request timeout 15s; Polly retry 3 lần, backoff 2s/4s/8s.
    /// </summary>
    public static IServiceCollection AddPower655Scrapers(this IServiceCollection services, Action<FetchOptions>? configure = null)
    {
        var options = new FetchOptions();
        configure?.Invoke(options);
        services.AddSingleton(options);

        services.AddHttpClient<VietlottSource>(Configure).AddPolicyHandler(RetryPolicy()).AddPolicyHandler(TimeoutPolicy());
        services.AddHttpClient<MinhNgocSource>(Configure).AddPolicyHandler(RetryPolicy()).AddPolicyHandler(TimeoutPolicy());

        services.AddTransient(sp => new DrawFetchService(
            sp.GetRequiredService<VietlottSource>(),
            sp.GetRequiredService<MinhNgocSource>(),
            sp.GetRequiredService<ILogger<DrawFetchService>>(),
            sp.GetRequiredService<FetchOptions>()));
        return services;
    }

    private static void Configure(HttpClient client)
    {
        client.Timeout = TimeSpan.FromSeconds(90); // tổng cho cả các lần retry
        client.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("vi-VN,vi;q=0.9");
    }

    private static IAsyncPolicy<HttpResponseMessage> RetryPolicy() =>
        HttpPolicyExtensions.HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

    private static IAsyncPolicy<HttpResponseMessage> TimeoutPolicy() =>
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(15));
}
