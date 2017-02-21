using System;
using System.Xml;

namespace Sensatus.FiberTracker.Setup.CustomActions
{
    /// <summary>
    /// Class Program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            var argCollection = args[0].Split(Convert.ToChar(","));
            var installPath = argCollection[0].Trim();
            var exeName = installPath + @"Sensatus.FiberTracker.UI.exe";
            var configFileName = installPath + @"\Sensatus.FiberTracker.UI.exe.config";
            ModifyAppConfig(configFileName, "connectionStrings", "connectionString", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + installPath + @"Database\Sensatus.FiberTracker.mdb;Jet OLEDB:Database Password=admin;");
            ModifyAppConfig(configFileName, "appSettings", 4, "value", argCollection[1]);
        }

        /// <summary>
        /// Modifies the application configuration.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        private static void ModifyAppConfig(string fileName, string sectionName, string attributeName, string value)
        {
            ModifyAppConfig(fileName, sectionName, 0, attributeName, value);
        }

        /// <summary>
        /// Modifies the application configuration.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="nodeIndex">Index of the node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        private static void ModifyAppConfig(string fileName, string sectionName, int nodeIndex, string attributeName, string value)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            var nodes = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
                if (string.Compare(node.Name, sectionName, true) == 0)
                {
                    node.ChildNodes[nodeIndex].Attributes[attributeName].Value = value.Trim();
                    break;
                }
            xmlDoc.Save(fileName);
        }
    }
}