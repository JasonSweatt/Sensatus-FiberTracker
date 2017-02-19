using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Diagnostics;

namespace Sensatus.FiberTracker.Setup.CustomActions
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            string[] argCollection = args[0].Split(Convert.ToChar(","));
            string installPath = argCollection[0].Trim();
            string exeName = installPath + @"Sensatus.FiberTracker.UI.exe";            

            string configFileName = installPath + @"\Sensatus.FiberTracker.UI.exe.config";            
            ModifyAppConfig(configFileName, "connectionStrings", "connectionString", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + installPath + @"Database\Sensatus.FiberTracker.mdb;Jet OLEDB:Database Password=admin;");
            ModifyAppConfig(configFileName, "appSettings", 4, "value", argCollection[1]);                        
        }


        private static void ModifyAppConfig(string fileName, string sectionName, string attributeName, string value)
        {
            ModifyAppConfig(fileName, sectionName, 0, attributeName, value);
        }
        private static void ModifyAppConfig(string fileName, string sectionName, int nodeIndex, string attributeName, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);


            XmlNodeList nodes = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                if (string.Compare(node.Name, sectionName, true) == 0)
                {
                    node.ChildNodes[nodeIndex].Attributes[attributeName].Value = value.Trim();
                    break;
                }
            }
            xmlDoc.Save(fileName);
        }


    }
}
