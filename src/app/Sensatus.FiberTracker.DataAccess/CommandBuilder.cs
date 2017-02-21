using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Sensatus.FiberTracker.DataAccess
{
    /// <summary>
    /// Class CommandBuilder.
    /// </summary>
    internal class CommandBuilder
    {
        /// <summary>
        /// The parameter builder
        /// </summary>
        private DBParamBuilder _paramBuilder = new DBParamBuilder();

        #region Inrernal Methods

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection)
        {
            return GetCommand(commandText, connection, CommandType.Text);
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType)
        {
            var command = GetCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.CommandType = commandType;
            return command;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameter parameter)
        {
            return GetCommand(commandText, connection, parameter, CommandType.Text);
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameter parameter, CommandType commandType)
        {
            var param = _paramBuilder.GetParameter(parameter);
            var command = GetCommand(commandText, connection, commandType);
            command.Parameters.Add(param);
            return command;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameterCollection parameterCollection)
        {
            return GetCommand(commandText, connection, parameterCollection, CommandType.Text);
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDbCommand.</returns>
        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameterCollection parameterCollection, CommandType commandType)
        {
            var paramArray = _paramBuilder.GetParameterCollection(parameterCollection);
            var command = GetCommand(commandText, connection, commandType);
            foreach (var param in paramArray)
                command.Parameters.Add(param);
            return command;
        }

        #endregion Inrernal Methods

        #region Private Methods

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <returns>IDbCommand.</returns>
        private IDbCommand GetCommand()
        {
            IDbCommand command = null;
            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    command = new SqlCommand();
                    break;

                case Common.MY_SQL_DB_PROVIDER:
                    command = new MySqlCommand();
                    break;

                case Common.ORACLE_DB_PROVIDER:
                    command = new OracleCommand();
                    break;

                case Common.EXCESS_DB_PROVIDER:
                    command = new OleDbCommand();
                    break;

                case Common.OLE_DB_PROVIDER:
                    command = new OleDbCommand();
                    break;

                case Common.ODBC_DB_PROVIDER:
                    command = new OdbcCommand();
                    break;
            }
            return command;
        }

        #endregion Private Methods
    }
}