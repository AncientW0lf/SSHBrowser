using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SSHBrowser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (_, _) => DisposeServerCaches();

            CreateHostBuilder(args).Build().Run();

            DisposeServerCaches();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
#if DEBUG
                    webBuilder.UseUrls("http://*:8080");
#else
                    webBuilder.UseUrls("http://*:80", "https://*:443");
#endif
                });

        private static void DisposeServerCaches()
        {
            IEnumerable<Type> caches = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace.Equals($"{nameof(SSHBrowser)}.{nameof(SSHBrowser.Server)}"));

            foreach (Type cache in caches)
                cache.GetMethod(nameof(IDisposable.Dispose)).Invoke(null, null);
        }
    }
}
