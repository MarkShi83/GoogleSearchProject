namespace GoogleSearch.Services
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The configuration.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The add services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IGoogleSearchService, GoogleSearchService>()
                .AddTransient<IDataService, DataService>()
                .AddTransient<IHttpClientService, HttpClientService>()
                .AddTransient<IAnalyseService, AnalyseService>();

            return services;
        }
    }
}
