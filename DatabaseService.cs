using System.Data.Common;
using Npgsql;

namespace csharp_mvc
{

    public class DatabaseService
    {
        public static NpgsqlConnection CreateConnection()
        {

            var connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Port = 5433,
                Username = "postgres",
                Password = "admin",
                Database = "todo_tasks"
            };

            return new NpgsqlConnection(connectionStringBuilder.ToString());
        }

    }

}