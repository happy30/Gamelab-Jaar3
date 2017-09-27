using UnityEngine;
using System.Collections.Generic;

using System.Xml.Serialization;
using System.IO;
using System.Text;

public static class XmlReader  {

    public static string RESOURCES_DIR = "Assets/Resources/";

    /// <summary>
    ///     Read a xml of name 'fileName' in the resources directory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName">
    ///     Name of the xml to load
    /// </param>
    /// <returns>
    ///     List of objects read in the xml
    /// </returns>
    public static List<T> LoadXML<T>(string fileName) where T : new() {

        TextAsset te = Resources.Load<TextAsset>(fileName);
        if (te == null) {
            return null;
        }

        string data;
        data = te.ToString();

        return dataToList<T>(data);    
    }

    /// <summary>
    ///     Read a xml outside the resources directory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName">
    ///     Name of the xml file
    /// </param>
    /// <returns>
    ///     List of objects read in the xml
    /// </returns>
    public static List<T> LoadXMLFrom<T>(string fileName) where T : new() {

        string data;

        try {
            FileStream fs = new FileStream(fileName + ".xml", FileMode.OpenOrCreate);
            Debug.Log(fs);
            TextReader reader = new StreamReader(fs, new UTF8Encoding());

            data = reader.ReadToEnd();

            reader.Close();
            fs.Close();
        } catch {
            data = "";
        }
        

        
        return dataToList<T>(data);
        
    }

    static List<T> dataToList<T>(string data)where T : new() {
        XmlBase<T> entitiesData;
        if (data == "") {
            entitiesData = new XmlBase<T>();
            entitiesData.attributes.Add(new T());
        } else {
            entitiesData = (XmlBase<T>)DeserializeObject<T>(data);
        }

        List<T> list = new List<T>();

        foreach (T ent in entitiesData.attributes) {
            list.Add(ent);
        }

        return list;
    }


    /// <summary>
    ///     Writes a xml file with the list of objects given
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="directory">
    ///     Directory to save
    /// </param>
    /// <param name="fileName">
    ///     Name of the xml file
    /// </param>
    /// <param name="obj">
    ///     List of objects to write
    /// </param>
    public static void SaveXmlTo<T>(string directory, string fileName, List<T> obj) {
        XmlSerializer xs = new XmlSerializer(typeof(XmlBase<T>));

        Directory.CreateDirectory(directory);

        FileStream fs = new FileStream(directory + fileName + ".xml", FileMode.Create);
        TextWriter writer = new StreamWriter(fs, new UTF8Encoding());

        XmlBase<T> data = new XmlBase<T>();
        data.attributes = obj;

        xs.Serialize(writer, data);
        writer.Close();
    }

    /*
    public static void SaveXml<T>(string fileName, List<T> obj) {
        SaveXmlTo<T>(RESOURCES_DIR, fileName, obj);
    }
    */
    
    static byte[] StringToUTF8ByteArray(string pXmlString) {
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] byteArray = encoding.GetBytes(pXmlString);
        return byteArray;
    }
    static object DeserializeObject<T>(string pXmlizedString) {

        XmlSerializer xs = new XmlSerializer(typeof(XmlBase<T>));
        MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
        //XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
        return xs.Deserialize(memoryStream);
    }


    [XmlRoot("Data")]
    public class XmlBase<T> {
        
        [XmlArray("List"), XmlArrayItem("Element")]
        public List<T> attributes = new List<T>();

        public XmlBase() {
            attributes = new List<T>();
        }
    }

    
}
