using System;
using System.Collections;
using System.Data;

namespace Sensatus.FiberTracker.DataAccess
{
    /// <summary>
    /// DBHelper class enables to execute Sql Objects for the connection parameters specified into web.config or App.config file.
    /// </summary>
    public class DBHelper
    {
        #region "Private Variables"

        private ConnectionManager _connectionManager = new ConnectionManager();
        private CommandBuilder _commandBuilder = new CommandBuilder();
        private DataAdapterManager _dbAdapterManager = new DataAdapterManager();

        #endregion "Private Variables"

        #region "Execute Scalar"

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            object objScalar = null;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, commandType);

            try
            {
                objScalar = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }
            return objScalar;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DBParameter param, CommandType commandType)
        {
            object objScalar = null;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, param, commandType);

            try
            {
                objScalar = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }
            return objScalar;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DBParameterCollection paramCollection, CommandType commandType)
        {
            object objScalar = null;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);

            try
            {
                objScalar = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }
            return objScalar;
        }

        /// <summary>
        /// Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="param">Parameter to be associated</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DBParameter param)
        {
            return ExecuteScalar(commandText, param, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and returns result.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Parameter collection to be associated.</param>
        /// <returns>A single value. (First row's first cell value, if more than one row and column is returned.)</returns>
        public object ExecuteScalar(string commandText, DBParameterCollection paramCollection)
        {
            return ExecuteScalar(commandText, paramCollection, CommandType.Text);
        }

        #endregion "Execute Scalar"

        #region ExecuteNonQuery

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            var iRowsEffected = 0;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, commandType);

            try
            {
                iRowsEffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }

            return iRowsEffected;
        }

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, DBParameter param, CommandType commandType)
        {
            var iRowsEffected = 0;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, param, commandType);

            try
            {
                iRowsEffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }
            return iRowsEffected;
        }

        /// <summary>
        /// Executes Sql Command or Stored procedure and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, DBParameterCollection paramCollection, CommandType commandType)
        {
            var iRowsEffected = 0;
            var connection = _connectionManager.GetConnection();
            var command = _commandBuilder.GetCommand(commandText, connection, paramCollection, commandType);

            try
            {
                iRowsEffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (command != null)
                    command.Dispose();
            }
            return iRowsEffected;
        }

        /// <summary>
        /// Executes Sql Command and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(commandText, CommandType.Text);
        }

        /// <summary>
        /// Executes Sql Command and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, DBParameter param)
        {
            return ExecuteNonQuery(commandText, param, CommandType.Text);
        }

        /// <summary>
        /// Executes Sql Command and returns number of rows effected.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Parameter Collection to be associated with the command</param>
        /// <returns>Number of rows effected.</returns>
        public int ExecuteNonQuery(string commandText, DBParameterCollection paramCollection)
        {
            return ExecuteNonQuery(commandText, paramCollection, CommandType.Text);
        }

        #endregion ExecuteNonQuery

        #region GetDataSet

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DBParameter param, CommandType commandType)
        {
            var daReturn = new DataSet();
            var connection = _connectionManager.GetConnection();

            var adapter = new DataAdapterManager().GetDataAdapter(commandText, connection, param, commandType);
            try
            {
                adapter.Fill(daReturn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return daReturn;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the command</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DBParameterCollection paramCollection, CommandType commandType)
        {
            var daReturn = new DataSet();
            var connection = _connectionManager.GetConnection();

            var adapter = new DataAdapterManager().GetDataAdapter(commandText, connection, paramCollection, commandType);
            try
            {
                adapter.Fill(daReturn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return daReturn;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, CommandType commandType)
        {
            var daReturn = new DataSet();
            var connection = _connectionManager.GetConnection();

            var adapter = new DataAdapterManager().GetDataAdapter(commandText, connection, commandType);
            try
            {
                adapter.Fill(daReturn);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return daReturn;
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(commandText, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <param name="param">Parameter to be associated with the command</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DBParameter param)
        {
            return ExecuteDataSet(commandText, param, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataSet.
        /// </summary>
        /// <param name="commandText">Sql Command </param>
        /// <param name="paramCollection">Parameter collection to be associated with the command</param>
        /// <returns>Result in the form of DataSet</returns>
        public DataSet ExecuteDataSet(string commandText, DBParameterCollection paramCollection)
        {
            return ExecuteDataSet(commandText, paramCollection, CommandType.Text);
        }

        #endregion GetDataSet

        #region "ExecuteDataTable"

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DBParameterCollection paramCollection, CommandType commandType)
        {
            var dtReturn = new DataTable();
            IDbConnection connection = null;
            try
            {
                connection = _connectionManager.GetConnection();
                dtReturn = _dbAdapterManager.GetDataTable(commandText, paramCollection, connection, tableName, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dtReturn;
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command8 or Stored Procedure name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DBParameterCollection paramCollection, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, paramCollection, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="tableName">Table name</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DBParameterCollection paramCollection)
        {
            return ExecuteDataTable(commandText, tableName, paramCollection, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="paramCollection">Parameter collection to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DBParameterCollection paramCollection)
        {
            return ExecuteDataTable(commandText, string.Empty, paramCollection, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DBParameter param, CommandType commandType)
        {
            return ExecuteDataTable(commandText, tableName, param, commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DBParameter param, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, param, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="tableName">Table name</param>
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, DBParameter param)
        {
            return ExecuteDataTable(commandText, tableName, param, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="param">Parameter to be associated with the Command.</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, DBParameter param)
        {
            return ExecuteDataTable(commandText, string.Empty, param, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="tableName">Table name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName, CommandType commandType)
        {
            return ExecuteDataTable(commandText, tableName, new DBParameterCollection(), commandType);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, string.Empty, commandType);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="tableName">Table name</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText, string tableName)
        {
            return ExecuteDataTable(commandText, tableName, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command and return resultset in the form of DataTable.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <returns>Result in the form of DataTable</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, string.Empty, CommandType.Text);
        }

        #endregion "ExecuteDataTable"

        #region "ExecuteReader"

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con, CommandType commandType)
        {
            IDataReader dataReader = null;
            var command = _commandBuilder.GetCommand(commandText, con, commandType);
            dataReader = command.ExecuteReader();
            command.Dispose();
            return dataReader;
        }

        /// <summary>
        /// Executes the Sql Command and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con)
        {
            return ExecuteDataReader(commandText, con, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="param">Parameter to be associated with the Sql Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con, DBParameter param, CommandType commandType)
        {
            IDataReader dataReader = null;
            var command = _commandBuilder.GetCommand(commandText, con, param, commandType);
            dataReader = command.ExecuteReader();
            command.Dispose();
            return dataReader;
        }

        /// <summary>
        /// Executes the Sql Command and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="param">Parameter to be associated with the Sql Command.</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con, DBParameter param)
        {
            return ExecuteDataReader(commandText, con, param, CommandType.Text);
        }

        /// <summary>
        /// Executes the Sql Command or Stored Procedure and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command or Stored Procedure Name</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="paramCollection">Parameter to be associated with the Sql Command or Stored Procedure.</param>
        /// <param name="commandType">Type of command (i.e. Sql Command/ Stored Procedure name/ Table Direct)</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con, DBParameterCollection paramCollection, CommandType commandType)
        {
            IDataReader dataReader = null;
            var command = _commandBuilder.GetCommand(commandText, con, paramCollection, commandType);
            dataReader = command.ExecuteReader();
            command.Dispose();
            return dataReader;
        }

        /// <summary>
        /// Executes the Sql Command and returns the DataReader.
        /// </summary>
        /// <param name="commandText">Sql Command</param>
        /// <param name="con">Database Connection object (DBHelper.GetConnObject() may be used)</param>
        /// <param name="paramCollection">Parameter to be associated with the Sql Command or Stored Procedure.</param>
        /// <returns>DataReader</returns>
        public IDataReader ExecuteDataReader(string commandText, IDbConnection con, DBParameterCollection paramCollection)
        {
            return ExecuteDataReader(commandText, con, paramCollection, CommandType.Text);
        }

        #endregion "ExecuteReader"

        /// <summary>
        /// Gets the data in array.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>System.String[].</returns>
        public string[] GetDataInArray(string query)
        {
            var dbResult = new string[20];
            var dataSet = ExecuteDataSet(query);
            for (var i = 0; i < 20; i++)
                dbResult[i] = string.Empty;

            for (var count = 0; count < dataSet.Tables[0].Columns.Count; count++)
                if (dataSet.Tables[0].Rows[0][count] != null)
                    dbResult[count] = dataSet.Tables[0].Rows[0][count].ToString().Trim();
                else
                    dbResult[count] = string.Empty;

            return dbResult;
        }

        /// <summary>
        /// Gets the data in array list.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <returns>ArrayList.</returns>
        public ArrayList GetDataInArrayList(string sqlCommand)
        {
            var dbResult = new ArrayList();
            var dataSet = ExecuteDataSet(sqlCommand);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return dbResult;
            for (var count = 0; count < dataSet.Tables[0].Columns.Count; count++)
                if (dataSet.Tables[0].Rows[0][count] != null)
                    if (dataSet.Tables[0].Rows[0][count].ToString() != string.Empty)
                        dbResult.Add(dataSet.Tables[0].Rows[0][count].ToString());
            return dbResult;
        }

        /// <summary>
        /// Creates and opens the database connection for the connection parameters specified into web.config or App.config file.
        /// </summary>
        /// <returns>Database connection object in the opened state. </returns>
        public IDbConnection GetConnObject()
        {
            return _connectionManager.GetConnection();
        }
    }
}