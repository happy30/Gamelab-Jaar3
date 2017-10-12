using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader1 : MonoBehaviour
{

    public const string path = "Conversations";
    public ConversationContainer cc;

    void Awake()
    {
        cc = ConversationContainer.Load(path);
    }


	
}
