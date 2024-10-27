using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimonCLI.Domain.Repositories;
using SimonCLI.Infra.Repositories;

namespace SimonCLI.Infra;
public static class InfraExtensions
{
	public static IServiceCollection ConfigureInfraServices(this IServiceCollection services)
	{
		services.AddTransient<DbConnectionService>(provider =>
		{
			string connectionString = provider.GetRequiredService<IConfiguration>().GetSection("ConnectionStrings:DefaultConnection").Value!;
			return new(connectionString);
		});

		services.AddScoped<IUsersRepository, UsersRepository>();

		return services;
	}
}
