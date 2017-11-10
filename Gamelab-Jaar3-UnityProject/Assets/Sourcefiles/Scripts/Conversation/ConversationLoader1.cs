using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader1 : MonoBehaviour
{

    public string path = "Conversations";
    public ConversationContainer cc;

    public ConversationContainer LoadConversation(string xmlName)
    {
        return ConversationContainer.Load(xmlName);
    }


	
}
