using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
namespace Sensatus.FiberTracker.BusinessLogic
{
    /// <summary>
    /// Class to be used for reading the Preferences.xml file
    /// </summary>
public class XMLHelper
{
    private XmlDocument xmlDocument = null;
    public XMLHelper()
    {
        xmlDocument = GetXMLDoc();
    }

    /// <summary>
    /// Reads the value of passed node name
    /// </summary>
    /// <param name="itemName">Node name for which vale needs to be read</param>
    /// <returns>Value of the node</returns>
    public string GetValue(string itemName)
    {
        string value = string.Empty;
        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;

        foreach (XmlNode node in nodes)
        {
            if (node.Attributes["name"].Value.Trim().ToLower() == itemName.Trim().ToLower())
            {
                value = node.Attributes["value"].Value.Trim();
                break; 
            }
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
        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;
        foreach (XmlNode node in nodes)
        {
            if (node.Attributes["name"].Value.Trim().ToLower() == itemName.Trim().ToLower())
            {
                node.Attributes["value"].Value = value.Trim();
                break; // TODO: might not be correct. Was : Exit For
            }
        }
        xmlDocument.Save(FileName);
    }

    /// <summary>
    /// Saves the modifications done into the XML Document.
    /// </summary>
    public void Save()
    {
        xmlDocument.Save(FileName);
        xmlDocument = GetXMLDoc();
    }

    /// <summary>
    /// Loads XML document for reading or writing purpose.
    /// </summary>
    public XmlDocument GetXMLDoc()
    {
        XmlDocument xmlDoc = new XmlDocument();
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
    private string FileName
    {
        get { return Application.StartupPath + "\\Preferences.xml"; }
    }
}
}