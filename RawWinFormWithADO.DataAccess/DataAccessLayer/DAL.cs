using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RawWinFormWithADO.DataAccess.Contracts;
using RawWinFormWithADO.DataAccess.Models;
using System.Data;


namespace RawWinFormWithADO.DataAccess.DataAccessLayer
{
    public class DAL : IDAL
    {
        private readonly string _connectionString;
        private readonly ILogger<DAL> _logger;

        public DAL(IOptions<AppSettingsForConnectionString> appSettings, ILogger<DAL> logger)
        {
            _connectionString = appSettings?.Value.DbCon
                ?? throw new ArgumentNullException(nameof(appSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Execute Methods
        public void Execute(string CommandName)
        {
            ExecuteCommand(CommandName, CommandType.Text, null);
        }

        public void Execute(string CommandName, SqlParameter[] parameters)
        {
            ExecuteCommand(CommandName, CommandType.Text, parameters);
        }

        public async Task ExecuteAsync(string CommandName)
        {
            await ExecuteCommandAsync(CommandName, CommandType.Text, null);
        }

        public async Task ExecuteAsync(string CommandName, SqlParameter[] parameters)
        {
            await ExecuteCommandAsync(CommandName, CommandType.Text, parameters);
        }
        #endregion

        #region Get Methods
        public DataTable Get(string CommandName)
        {
            return GetData(CommandName, CommandType.Text, null);
        }

        public DataTable Get(string CommandName, SqlParameter[] parameters)
        {
            return GetData(CommandName, CommandType.Text, parameters);
        }

        public async Task<DataTable> GetAsync(string CommandName)
        {
            return await GetDataAsync(CommandName, CommandType.Text, null);
        }

        public async Task<DataTable> GetAsync(string CommandName, SqlParameter[] parameters)
        {
            return await GetDataAsync(CommandName, CommandType.Text, parameters);
        }

        public DataSet GetByProcedureAdapterDS(string CommandName, SqlParameter[] parameters)
        {
            return GetDataSet(CommandName, CommandType.StoredProcedure, parameters);
        }
        #endregion

        #region Private Helper Methods
        private void ExecuteCommand(string commandName, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = CreateCommand(commandName, commandType, connection, parameters))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing command: {CommandName}", commandName);
                throw;
            }
        }

        private async Task ExecuteCommandAsync(string commandName, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = CreateCommand(commandName, commandType, connection, parameters))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing command asynchronously: {CommandName}", commandName);
                throw;
            }
        }

        private DataTable GetData(string commandName, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = CreateCommand(commandName, commandType, connection, parameters))
                {
                    connection.Open();
                    var dataTable = new DataTable();
                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data: {CommandName}", commandName);
                throw;
            }
        }

        private async Task<DataTable> GetDataAsync(string commandName, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = CreateCommand(commandName, commandType, connection, parameters))
                {
                    await connection.OpenAsync();
                    var dataTable = new DataTable();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data asynchronously: {CommandName}", commandName);
                throw;
            }
        }

        private DataSet GetDataSet(string commandName, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = CreateCommand(commandName, commandType, connection, parameters))
                {
                    var dataSet = new DataSet();
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving DataSet: {CommandName}", commandName);
                throw;
            }
        }

        private SqlCommand CreateCommand(string commandName, CommandType commandType, SqlConnection connection, SqlParameter[] parameters)
        {
            var command = new SqlCommand(commandName, connection)
            {
                CommandType = commandType,
                CommandTimeout = 600 // Default timeout in seconds
            };

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
        #endregion
    }
}
