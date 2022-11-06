using Dapper;
using MySql.Data.MySqlClient;
using System.Data;


namespace DataAccessLibrary
{
    /// <summary>
    /// This class represents the MySQL data access layer
    /// </summary>
    internal class MySQLDataAccess
    {
        private readonly string _connectionString;

        public MySQLDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Retrieve data from data layer
        /// </summary>
        /// <typeparam name="T">Output type</typeparam>
        /// <typeparam name="U">Parameter type</typeparam>
        /// <param name="sqlStatement">sql statement</param>
        /// <param name="parameters">query parameters</param>
        /// <param name="isRoutine">determine wether sql statement is a stored procedure. Default = true</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> LoadData<T, U>(string sqlStatement, U parameters, bool isRoutine = true)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);
            if (isRoutine)
            {
                return connection.QueryAsync<T>(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
            }
            return connection.QueryAsync<T>(sqlStatement, parameters);
        }

        /// <summary>
        /// Save data to data layer
        /// </summary>
        /// <typeparam name="T">Input type. Usually infered from params</typeparam>
        /// <param name="sqlStatement">sql statement</param>
        /// <param name="parameters">query parameters</param>
        /// <param name="isRoutine">determine wether sql statement is a stored procedure. Default = true</param>
        /// <returns></returns>
        public Task SaveData<T>(string sqlStatement, T parameters, bool isRoutine = true)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);
            if (isRoutine)
            {
                return connection.ExecuteAsync(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
            }
            return connection.ExecuteAsync(sqlStatement, parameters);
        }
    }
}
