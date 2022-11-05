using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<IEnumerable<T>> LoadData<T, U>(string sqlStatement, U parameters, bool isRoutine = true)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);
            if (isRoutine)
            {
                return connection.QueryAsync<T>(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
            }
            return connection.QueryAsync<T>(sqlStatement, parameters);
        }

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
