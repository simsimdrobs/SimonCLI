using Microsoft.Data.Sqlite;

namespace SimonCLI.Infra;
internal class DbConnectionService(string connectionString)
{
	public SqliteConnection CreateConnection()
	{
		return new SqliteConnection(connectionString);
	}
}
