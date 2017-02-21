using System.Reflection;

namespace Sensatus.FiberTracker.UI
{
    /// <summary>
    /// Class provides the information about product assembly.
    /// </summary>
    public static class ApplicationDetails
    {
        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets title of the assembly.
        /// </summary>
        /// <value>The assembly title.</value>
        public static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length <= 0)
                    return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                return titleAttribute.Title != string.Empty ? titleAttribute.Title : System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets version of the assembly.
        /// </summary>
        /// <value>The assembly version.</value>
        public static string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Gets description of the assembly.
        /// </summary>
        /// <value>The assembly description.</value>
        public static string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets product name of the assembly.
        /// </summary>
        /// <value>The assembly product.</value>
        public static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets copyright for the assembly.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets name of the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public static string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion Assembly Attribute Accessors
    }
}