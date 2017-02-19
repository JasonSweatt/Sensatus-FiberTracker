using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Text;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Sensatus.FiberTracker.DataAccess
{
    internal class CommandBuilder
    {
        private DBParamBuilder _paramBuilder = new DBParamBuilder();

        #region Inrernal Methods
        internal IDbCommand GetCommand(string commandText, IDbConnection connection)
        {
            return GetCommand(commandText, connection, CommandType.Text);
        }


        internal IDbCommand GetCommand(string commandText, IDbConnection connection, CommandType commandType)
        {
            IDbCommand command = GetCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.CommandType = commandType;
            return command;
        }


        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameter parameter)
        {
            return GetCommand(commandText, connection, parameter, CommandType.Text);
        }

        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameter parameter, CommandType commandType)
        {
            IDataParameter param = _paramBuilder.GetParameter(parameter);
            IDbCommand command = GetCommand(commandText, connection, commandType);
            command.Parameters.Add(param);
            return command;
        }

        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameterCollection parameterCollection)
        {
            return GetCommand(commandText, connection, parameterCollection, CommandType.Text);
        }

        internal IDbCommand GetCommand(string commandText, IDbConnection connection, DBParameterCollection parameterCollection, CommandType commandType)
        {
            List<IDataParameter> paramArray = _paramBuilder.GetParameterCollection(parameterCollection);
            IDbCommand command = GetCommand(commandText, connection, commandType);

            foreach (IDataParameter param in paramArray)            
                command.Parameters.Add(param);
            
            return command;
        }


        #endregion

        #region Private Methods"

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

        #endregion
    }
}
