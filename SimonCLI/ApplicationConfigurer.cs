using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimonCLI.Infra;
using System;

namespace SimonCLI;
internal class ServicesConfigurer
{
	public static void ConfigureServices(IServiceCollection services)
	{
		services.ConfigureInfraServices();
	}
}

internal class AppConfigurer
{
	public static void ConfigureApplication(IConfigurationBuilder configurationBuilder)
	{
		configurationBuilder
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
	}
}