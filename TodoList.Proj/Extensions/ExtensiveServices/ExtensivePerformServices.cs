using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace TodoList.Proj.ExtensionMethods;

public static class ExtensivePerformServices
{
    public static void PerformaceServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddResponseCompression
        (options =>
            options.Providers.Add<GzipCompressionProvider>()
        );
        builder.Services.Configure<GzipCompressionProviderOptions>(
            x =>
            {
                x.Level = CompressionLevel.Optimal;
            });
    }

}