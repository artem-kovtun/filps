using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Filps.Domain.Configurations;
using Filps.Domain.Extensions;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
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
        
        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<T> ExecuteScalarProcedure<T>(string procedureName, object parameters = null)
        {
            await using var connection = GetSqlConnection();
            await using var command = new SqlCommand(procedureName, connection);
            
            if (parameters != null) 
                command.Parameters.AddRange(parameters.ToSqlParamsArray());
            
            command.CommandType = CommandType.StoredProcedure;
            
            await connection.OpenAsync();
            
            var result = await command.ExecuteScalarAsync();
            
            return result is T typedResult ? typedResult : default;
        }
        
        public async Task ExecuteNonQueryProcedure(string procedureName, object parameters = null)
        {
            await using var connection = GetSqlConnection();
            await using var command = new SqlCommand(procedureName, connection);
            
            if (parameters != null)
                command.Parameters.AddRange(parameters.ToSqlParamsArray());
            
            command.CommandType = CommandType.StoredProcedure;
            
            await connection.OpenAsync();

            await command.ExecuteNonQueryAsync();
        }
        
        public async Task<string> ExecuteJsonQueryAsync(string query, object parameters, CommandType commandType)
        {
            var res = new StringBuilder();
            
            await using var connection = GetSqlConnection();
            await using var command = new SqlCommand(query, connection) {CommandType = commandType};
            
            if (parameters != null) command.Parameters.AddRange(parameters.ToSqlParamsArray());
                
            await connection.OpenAsync();
            
            var reader = await command.ExecuteReaderAsync();
            
            while (reader.Read())
            {
                res.Append(reader.GetString(0));
            }
            
            return res.ToString();
        }
        
        public async Task<T> ExecuteJsonResultProcedureAsync<T>(string query, object sqlParams = null)
        {
            var json = await ExecuteJsonQueryAsync(query, sqlParams, CommandType.StoredProcedure);
            return JsonConvert.DeserializeObject<T>(json);
        }


    }
}