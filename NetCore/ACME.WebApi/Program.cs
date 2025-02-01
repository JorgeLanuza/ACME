namespace ACME.WebApi
{
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    public class Program
    {
        private const string CONFIGURATION_FILE_NAME = "appsettings.json";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(currentPath)
                .AddJsonFile(CONFIGURATION_FILE_NAME, optional: false, reloadOnChange: true);
            IHostBuilder host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(configurationBuilder.Build());
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseIISIntegration();
                });
            return host;
        }
    }
}