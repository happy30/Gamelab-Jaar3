//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreUI : MonoBehaviour
{

    public GameObject interactCursor;

    public void ActivateExploreUI()
    {

    }

    public void ShowInteractCursor(bool show)
    {
        if(PlayMode.gameMode == PlayMode.GameMode.Explore && show)
        {
            interactCursor.SetActive(true);
            return;
        }
        interactCursor.SetActive(false);
    }
}
   

    
