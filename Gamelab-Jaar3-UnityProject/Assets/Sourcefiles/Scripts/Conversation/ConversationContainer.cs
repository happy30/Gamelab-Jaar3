//ConversationContainer by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

[XmlRoot("List")]
public class ConversationContainer : MonoBehaviour
{

    [XmlArray("Element")]
    [XmlArrayItem("lines")]
    public List<Conversation> messages = new List<Conversation>();


    //Load XML File
    public static ConversationContainer Load(string path)
    {
        Debug.Log(Application.dataPath + path);
        TextAsset _xml = Resources.Load<TextAsset>(path);

        Debug.Log(_xml);

        XmlSerializer serializer = new XmlSerializer(typeof(ConversationContainer));

        StringReader reader = new StringReader(_xml.text);

        ConversationContainer messages = serializer.Deserialize(reader) as ConversationContainer;

        reader.Close();

        return messages;
    }
}
