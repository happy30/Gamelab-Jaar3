//ConversationLoader.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader: MonoBehaviour
{
    public const string path = "XML_Editor/Resources";

    void Start()
    {
        ConversationContainer cc = ConversationContainer.Load(path);
    }
}
