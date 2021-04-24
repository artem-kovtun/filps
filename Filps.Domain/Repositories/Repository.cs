using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Filps.Domain.Configurations;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;

namespace Filps.Domain.Repositories
{
    public class Repository
    {
        protected string ConnectionString { get; }
        
        protected Repository(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            ConnectionString = databaseConfiguration.Value.ConnectionString;
        }

        protected Task<IEnumerable<T>> QueryAsync<T>(string functionName, object parameters)
        {
            using var db = new NpgsqlConnection(ConnectionString);
            return db.QueryAsync<T>(functionName, parameters, commandType: CommandType.StoredProcedure);
        }
        
        protected async Task<T> QueryFirstOrDefaultAsync<T>(string functionName, object parameters)
        {
            await using var db = new NpgsqlConnection(ConnectionString);
            return await db.QueryFirstOrDefaultAsync<T>(functionName, parameters, commandType: CommandType.StoredProcedure);
        }
        
        protected Task ExecuteAsync(string functionName, object parameters)
        {
            using var db = new NpgsqlConnection(ConnectionString);
            return db.ExecuteAsync(functionName, parameters, commandType: CommandType.StoredProcedure);
        }
        
        protected async Task<T> QueryJsonAsync<T>(string functionName, object parameters)
        {
            await using var db = new NpgsqlConnection(ConnectionString);
            var json = await db.QueryFirstOrDefaultAsync<string>(functionName, parameters, commandType: CommandType.StoredProcedure);
            return JsonConvert.DeserializeObject<T>(json);
        }


    }
}