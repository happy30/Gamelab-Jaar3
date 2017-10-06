using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationLoader : MonoBehaviour
{

    public const string path = "Conversations";

    void Start()
    {
        ConversationContainer cc = ConversationContainer.Load(path);

        print(cc.interactions[0].lines[0].actors.actor);
    }
}
