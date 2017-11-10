//ItemContainer.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

[XmlRoot("Data")]
public class ConversationContainer
{
    [XmlArray("List")]
    [XmlArrayItem("Element")]
    public List<Conversation> interactions = new List<Conversation>();

    //Load XML File
    public static ConversationContainer Load(string path)
    {
        Debug.Log(Application.dataPath + path);
        TextAsset _xml = Resources.Load<TextAsset>(path);

        Debug.Log(_xml);

        XmlSerializer serializer = new XmlSerializer(typeof(ConversationContainer));

        StringReader reader = new StringReader(_xml.text);

        ConversationContainer cc = serializer.Deserialize(reader) as ConversationContainer;

        reader.Close();

        return cc;
    }

}
