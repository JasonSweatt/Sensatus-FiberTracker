using System;
using System.Windows.Forms;
using System.Xml;

namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class to be used for reading the Preferences.xml file
    /// </summary>
    public class XMLHelper
    {
        /// <summary>
        /// The XML document
        /// </summary>
        private XmlDocument _xmlDocument = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLHelper"/> class.
        /// </summary>
        public XMLHelper()
        {
            _xmlDocument = GetXMLDoc();
        }

        /// <summary>
        /// Reads the value of passed node name
        /// </summary>
        /// <param name="itemName">Node name for which vale needs to be read</param>
        /// <returns>Value of the node</returns>
        public string GetValue(string itemName)
        {
            var value = string.Empty;
            var nodes = _xmlDocument.DocumentElement.ChildNodes;

            foreach (XmlNode node in nodes)
                if (node.Attributes["name"].Value.Trim().ToLower() == itemName.Trim().ToLower())
                {
                    value = node.Attributes["value"].Value.Trim();
                    break;
                }

            return value;
        }

        /// <summary>
        /// Saves the value for the passed xml node into Preference.xml file
        /// </summary>
        /// <param name="itemName">XML Node name</param>
        /// <param name="value">value to be saved</param>
        public void SetValue(string itemName, string value)
        {
            var nodes = _xmlDocument.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodes)
                if (node.Attributes["name"].Value.Trim().ToLower() == itemName.Trim().ToLower())
                {
                    node.Attributes["value"].Value = value.Trim();
                    break; // TODO: might not be correct. Was : Exit For
                }
            _xmlDocument.Save(FileName);
        }

        /// <summary>
        /// Saves the modifications done into the XML Document.
        /// </summary>
        public void Save()
        {
            _xmlDocument.Save(FileName);
            _xmlDocument = GetXMLDoc();
        }

        /// <summary>
        /// Loads XML document for reading or writing purpose.
        /// </summary>
        /// <returns>XmlDocument.</returns>
        public XmlDocument GetXMLDoc()
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return xmlDoc;
        }

        /// <summary>
        /// Gets the User Preference file name.
        /// </summary>
        /// <value>The name of the file.</value>
        private string FileName => Application.StartupPath + "\\Preferences.xml";
    }
}