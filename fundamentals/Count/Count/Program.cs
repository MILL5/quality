using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Pineapple.Diagnostics;

namespace Count
{
    public static class Program
    {
        private static ServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _serviceProvider.GetService<CountApp>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(o =>
            {
                o.SetMinimumLevel(LogLevel.Trace);
                o.AddConsole();
            });

            serviceCollection.AddTransient<CountApp>();
        }

        public static void Flush(this ILogger log)
        {
            _serviceProvider.Dispose();
        }
    }
}
