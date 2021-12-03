using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace MyService
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureAppSettings(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                var environment = hostingContext.HostingEnvironment;

                configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($" appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                if (environment.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(environment.ApplicationName));
                    if (appAssembly != null)
                    {
                        configuration.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                configuration.AddEnvironmentVariables();
            });

            return builder;
        }
    }
}
