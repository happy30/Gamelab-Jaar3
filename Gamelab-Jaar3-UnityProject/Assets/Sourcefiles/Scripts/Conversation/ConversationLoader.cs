//ConversationLoader.cs by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader: MonoBehaviour
{
    public const string path = "Conversations";

    void Start()
    {
        ConversationContainer cc = ConversationContainer.Load(path);

        print(cc.messages[0].actor);
    }
}
