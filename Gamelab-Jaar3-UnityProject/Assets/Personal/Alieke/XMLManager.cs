using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class XMLManager
{
    [XmlElement("TestNumber")]
    public int number;

    [XmlElement("TestPosition")]
    public Vector3 position;



    public XMLManager()
    {

    }

    public XMLManager(int testNumber,Vector3 testPosition)
    {
        number = testNumber;
        position = testPosition;
    }
}
