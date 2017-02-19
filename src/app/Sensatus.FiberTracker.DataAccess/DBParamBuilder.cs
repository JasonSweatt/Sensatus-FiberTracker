using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Text;
using MySql.Data.MySqlClient;

namespace Sensatus.FiberTracker.DataAccess
{
    internal class DBParamBuilder
    {
        internal IDataParameter GetParameter(DBParameter parameter)
        {            
            IDbDataParameter dbParam = GetParameter();         
            dbParam.ParameterName = parameter.Name;         
            dbParam.Value = parameter.Value;
            dbParam.Direction = parameter.ParamDirection;            
            dbParam.DbType = parameter.Type;

            return dbParam;            
        }

        internal List<IDataParameter> GetParameterCollection(DBParameterCollection parameterCollection)
        {
            List<IDataParameter> dbParamCollection = new List<IDataParameter>();
            IDataParameter dbParam = null;
            foreach(DBParameter param in parameterCollection.Parameters)
            {
                dbParam = GetParameter(param);
                dbParamCollection.Add(dbParam);
            }
            
            return dbParamCollection;
        }

        #region Private Methods
        private IDbDataParameter GetParameter()
        {
            IDbDataParameter dbParam = null;
            switch (Configuration.DBProvider.Trim().ToUpper())
            {
                case Common.SQL_SERVER_DB_PROVIDER:
                    dbParam = new SqlParameter();
                    break;
                case Common.MY_SQL_DB_PROVIDER:
                    dbParam = new MySqlParameter();
                    break;
                case Common.ORACLE_DB_PROVIDER:
                    dbParam = new OracleParameter();
                    break;
                case Common.EXCESS_DB_PROVIDER:
                    dbParam = new OleDbParameter();
                    break;
                case Common.OLE_DB_PROVIDER:
                    dbParam = new OleDbParameter();
                    break;
                case Common.ODBC_DB_PROVIDER:
                    dbParam = new OdbcParameter();
                    break;
            }
            return dbParam;
        }

  

        #endregion
    }
}
