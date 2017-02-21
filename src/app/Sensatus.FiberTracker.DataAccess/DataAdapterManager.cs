using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Sensatus.FiberTracker.DataAccess
{
    internal class DataAdapterManager
    {
        /// <summary>
        /// Gets the data adapter.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>IDataAdapter.</returns>
        internal IDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection)
        {
            return GetDataAdapter(sqlCommand, connection, CommandType.Text);
        }

        /// <summary>
        /// Gets the data adapter.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDataAdapter.</returns>
        internal IDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, DBParameter param, CommandType commandType)
        {
            IDataAdapter adapter = null;
            var command = new CommandBuilder().GetCommand(sqlCommand, connection, param, commandType);

            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    adapter = new SqlDataAdapter((SqlCommand)command);
                    break;

                case Common.MY_SQL_DB_PROVIDER:
                    adapter = new MySqlDataAdapter((MySqlCommand)command);
                    break;

                case Common.ORACLE_DB_PROVIDER:
                    adapter = new OracleDataAdapter((OracleCommand)command);
                    break;

                case Common.EXCESS_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.OLE_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.ODBC_DB_PROVIDER:
                    adapter = new OdbcDataAdapter((OdbcCommand)command);
                    break;
            }

            return adapter;
        }

        /// <summary>
        /// Gets the data adapter.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="paramCollection">The parameter collection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDataAdapter.</returns>
        internal IDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, DBParameterCollection paramCollection, CommandType commandType)
        {
            IDataAdapter adapter = null;
            var command = new CommandBuilder().GetCommand(sqlCommand, connection, paramCollection, commandType);

            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    adapter = new SqlDataAdapter((SqlCommand)command);
                    break;

                case Common.MY_SQL_DB_PROVIDER:
                    adapter = new MySqlDataAdapter((MySqlCommand)command);
                    break;

                case Common.ORACLE_DB_PROVIDER:
                    adapter = new OracleDataAdapter((OracleCommand)command);
                    break;

                case Common.EXCESS_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.OLE_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.ODBC_DB_PROVIDER:
                    adapter = new OdbcDataAdapter((OdbcCommand)command);
                    break;
            }

            return adapter;
        }

        /// <summary>
        /// Gets the data adapter.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>IDataAdapter.</returns>
        internal IDataAdapter GetDataAdapter(string sqlCommand, IDbConnection connection, CommandType commandType)
        {
            IDataAdapter adapter = null;
            var command = new CommandBuilder().GetCommand(sqlCommand, connection, commandType);

            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    adapter = new SqlDataAdapter((SqlCommand)command);
                    break;

                case Common.MY_SQL_DB_PROVIDER:
                    adapter = new MySqlDataAdapter((MySqlCommand)command);
                    break;

                case Common.ORACLE_DB_PROVIDER:
                    adapter = new OracleDataAdapter((OracleCommand)command);
                    break;

                case Common.EXCESS_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.OLE_DB_PROVIDER:
                    adapter = new OleDbDataAdapter((OleDbCommand)command);
                    break;

                case Common.ODBC_DB_PROVIDER:
                    adapter = new OdbcDataAdapter((OdbcCommand)command);
                    break;
            }

            return adapter;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="paramCollection">The parameter collection.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>DataTable.</returns>
        internal DataTable GetDataTable(string sqlCommand, DBParameterCollection paramCollection, IDbConnection connection, string tableName, CommandType commandType)
        {
            var dataTable = tableName != string.Empty ? new DataTable(tableName) : new DataTable();
            var command = (paramCollection != null)
                ? paramCollection.Parameters.Count > 0
                    ? new CommandBuilder().GetCommand(sqlCommand, connection, paramCollection, commandType)
                    : new CommandBuilder().GetCommand(sqlCommand, connection, commandType)
                : new CommandBuilder().GetCommand(sqlCommand, connection, commandType);

            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    var sqlDataAdapter = new SqlDataAdapter((SqlCommand)command);
                    try
                    {
                        sqlDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex1)
                    {
                        throw ex1;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (sqlDataAdapter != null)
                            sqlDataAdapter.Dispose();
                    }
                    break;

                case Common.MY_SQL_DB_PROVIDER:
                    var mySqlDataAdapter = new MySqlDataAdapter((MySqlCommand)command);
                    try
                    {
                        mySqlDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex2)
                    {
                        throw ex2;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (mySqlDataAdapter != null)
                            mySqlDataAdapter.Dispose();
                    }
                    break;

                case Common.ORACLE_DB_PROVIDER:
                    var oracleDataAdapter = new OracleDataAdapter((OracleCommand)command);
                    try
                    {
                        oracleDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex3)
                    {
                        throw ex3;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (oracleDataAdapter != null)
                            oracleDataAdapter.Dispose();
                    }
                    break;

                case Common.EXCESS_DB_PROVIDER:
                    var oleDbDataAdapter = new OleDbDataAdapter((OleDbCommand)command);
                    try
                    {
                        oleDbDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex4)
                    {
                        throw ex4;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (oleDbDataAdapter != null)
                            oleDbDataAdapter.Dispose();
                    }
                    break;

                case Common.OLE_DB_PROVIDER:
                    var dbDataAdapter = new OleDbDataAdapter((OleDbCommand)command);
                    try
                    {
                        dbDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex4)
                    {
                        throw ex4;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (dbDataAdapter != null)
                            dbDataAdapter.Dispose();
                    }
                    break;

                case Common.ODBC_DB_PROVIDER:
                    var odbcDataAdapter = new OdbcDataAdapter((OdbcCommand)command);
                    try
                    {
                        odbcDataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex4)
                    {
                        throw ex4;
                    }
                    finally
                    {
                        if (command != null)
                            command.Dispose();

                        if (odbcDataAdapter != null)
                            odbcDataAdapter.Dispose();
                    }
                    break;
            }
            return dataTable;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>DataTable.</returns>
        internal DataTable GetDataTable(string sqlCommand, DBParameter param, IDbConnection connection, string tableName, CommandType commandType)
        {
            var paramCollection = new DBParameterCollection();
            paramCollection.Add(param);
            return GetDataTable(sqlCommand, paramCollection, connection, tableName, commandType);
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>DataTable.</returns>
        internal DataTable GetDataTable(string sqlCommand, IDbConnection connection, string tableName, CommandType commandType)
        {
            return GetDataTable(sqlCommand, new DBParameterCollection(), connection, tableName, commandType);
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>DataTable.</returns>
        internal DataTable GetDataTable(string sqlCommand, IDbConnection connection, CommandType commandType)
        {
            return GetDataTable(sqlCommand, new DBParameterCollection(), connection, string.Empty, commandType);
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="connection">The connection.</param>
        /// <returns>DataTable.</returns>
        internal DataTable GetDataTable(string sqlCommand, IDbConnection connection)
        {
            return GetDataTable(sqlCommand, new DBParameterCollection(), connection, string.Empty, CommandType.Text);
        }
    }
}