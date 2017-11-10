using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour
{
    XMLManager xmlSaved = new XMLManager();
    XMLManager xmlToSave;

    public int testInt;
    public Transform testPos;


    void Awake()
    {
        xmlToSave = new XMLManager(testInt, testPos.position);
    }

    public void LoadData()
    {
        xmlSaved = Streamdata();
        testInt = xmlSaved.number;
        testPos.position = xmlSaved.position;
    }

    public void SaveData()
    {
        xmlSaved = WriteData(xmlToSave);
        xmlSaved.number = testInt;
        xmlSaved.position = testPos.position;
    }

    public XMLManager Streamdata()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/Personal/Alieke/XML_File.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(XMLManager));
        XMLManager xmlManager = serializer.Deserialize(reader) as XMLManager;
        reader.Close();
        return xmlManager;
    }

    public XMLManager WriteData(XMLManager manager)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Personal/Alieke/XML_File.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(XMLManager));
        serializer.Serialize(writer, manager);
        writer.Close();
        return manager;
    }
}
