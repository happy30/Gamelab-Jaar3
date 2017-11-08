using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneProgression : MonoBehaviour
{
    public int progression;
    public int startProgression;

    public SceneObjects sceneObjects;

    public enum Scene
    {
        _144_01_Story,
        _144_02_Escape,
    };

    public Scene scene;

    void Awake()
    {
        sceneObjects = GetComponent<SceneObjects>();
    }

    void Start()
    {
        progression = startProgression;
    }

    public void ProgressionEffect (int prog)
    {
        switch(scene)
        {
            case Scene._144_01_Story:

                switch (prog)
                {
                    case 0:
                        sceneObjects.ActivateObject(0);
                        break;

                }


                break;
        }
    }
}
