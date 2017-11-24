//DontDestroyOnLoad by Jordi

using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad Instance;
    public bool enable;

    //Adds DontDestroyOnLoad on GameObject. If object already exists delete to prevent having two
    void Awake()
    {
        if(enable)
        {
            if (Instance)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }

    }
}
