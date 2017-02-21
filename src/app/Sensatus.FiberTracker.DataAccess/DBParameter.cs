using System.Data;

namespace Sensatus.FiberTracker.DataAccess
{
    public class DBParameter
    {
        #region "Private Variables"

        #endregion "Private Variables"

        #region "Constructors"

        /// <summary>
        /// Default constructor. Parameter name, vale, type and direction needs to be assigned explicitly by using the public properties exposed.
        /// </summary>
        public DBParameter()
        {
        }

        /// <summary>
        /// Creates a parameter with the name and value specified. Default data type and direction is String and Input respectively.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        public DBParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Creates a parameter with the name, value and direction specified. Default data type is String.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="paramDirection">Parameter direction</param>
        public DBParameter(string name, object value, ParameterDirection paramDirection)
        {
            Name = name;
            Value = value;
            ParamDirection = paramDirection;
        }

        /// <summary>
        /// Creates a parameter with the name, value and Data type specified. Default direction is Input.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="dbType">Data type</param>
        public DBParameter(string name, object value, DbType dbType)
        {
            Name = name;
            Value = value;
            Type = dbType;
        }

        /// <summary>
        /// Creates a parameter with the name, value, data type and direction specified.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value associated with the parameter</param>
        /// <param name="dbType">Data type</param>
        /// <param name="paramDirection">Parameter direction</param>
        public DBParameter(string name, object value, DbType dbType, ParameterDirection paramDirection)
        {
            Name = name;
            Value = value;
            Type = dbType;
            ParamDirection = paramDirection;
        }

        #endregion "Constructors"

        #region "Public Properties"

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the value associated with the parameter.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; set; } = null;

        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        /// <value>The type.</value>
        public DbType Type { get; set; } = DbType.String;

        /// <summary>
        /// Gets or sets the direction of the parameter.
        /// </summary>
        /// <value>The parameter direction.</value>
        public ParameterDirection ParamDirection { get; set; } = ParameterDirection.Input;

        #endregion "Public Properties"
    }
}