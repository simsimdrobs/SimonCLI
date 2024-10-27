using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimonCLI;
using System;


var host = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(AppConfigurer.ConfigureApplication)
	.ConfigureServices(ServicesConfigurer.ConfigureServices)
	.Build();

var config = host.Services.GetRequiredService<IConfiguration>();

Console.WriteLine(config["ConnectionStrings:DefaultConnection"]);
Console.WriteLine(host.Services.GetRequiredService<IHostEnvironment>().EnvironmentName);
