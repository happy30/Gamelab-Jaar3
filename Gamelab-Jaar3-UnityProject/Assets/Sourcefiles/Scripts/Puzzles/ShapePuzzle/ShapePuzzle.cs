using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePuzzle : MonoBehaviour
{
    int counter;
    AudioSource source;
    public AudioClip click;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Count()
    {
        source.PlayOneShot(click);
        counter++;
        if(counter == 3)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        SceneProgression sp = GameObject.Find("SceneSettings").GetComponent<SceneProgression>();


        GameObject.Find("SceneSettings").GetComponent<SceneEventData>().shapePuzzleCompleted = true;
        sp.progression++;
        sp.ProgressionEffect(sp.progression);
        GameObject.Find("SceneSettings").GetComponent<SceneEventData>().interactions[1].Trigger(true);
        //computer.GetComponent<Collider>().enabled = false;
        //sceneBlock.SetActive(false);
        //block.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
