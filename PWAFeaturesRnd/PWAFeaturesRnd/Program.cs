using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace PWAFeaturesRnd
{
	public class Program
	{
		private static string _env = "AzureBuild";

		public static void Main(string[] args)
		{
			SetEnvironment();
			CreateHostBuilder(args).Build().Run();
		}

		public static void SetEnvironment()
		{
			try
			{
				var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build();
				_env = config.GetSection("Environment").Value;
			}
			catch (Exception ex)
			{
				_env = "AzureBuild";
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				config.AddJsonFile("appsettings.json");
				config.AddJsonFile($"appsettings.{_env}.json", optional: true);
			})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>()
					.ConfigureLogging((logging) =>
					{
						logging.ClearProviders();
						logging.SetMinimumLevel(LogLevel.Warning);
					}).UseNLog();
				});
	}
}
