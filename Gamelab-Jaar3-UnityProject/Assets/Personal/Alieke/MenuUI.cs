//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuCanvas;
    public PlayMode.GameMode lastMode;
    public MenuManager.MenuState menuState;

    public GameObject conversation;
    public GameObject explore;



    private bool paused;
    public GameObject activePanel;
	
	void Update ()
    {
        if (!paused)
        {
            if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.F))
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    menuState = MenuManager.MenuState.System;
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    menuState = MenuManager.MenuState.Items;
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    menuState = MenuManager.MenuState.Flow;
                }
                
                Pause(menuState, false);
            }
           
        }
        else if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.F))
        {
            Resume();
        }

	}

    public void Pause(MenuManager.MenuState state, bool exploreMode)
    {
        //GetComponent<CanvasGroup>().alpha = 0;
        conversation.SetActive(false);
        explore.SetActive(false);

        if(!exploreMode)
        {
            lastMode = PlayMode.gameMode;
        }
        else
        {
            lastMode = PlayMode.GameMode.Explore;
        }
        
        PlayMode.ChangeGameMode(PlayMode.GameMode.Menu);
        menuCanvas.SetActive(true);
        menuCanvas.GetComponent<MenuManager>().SetMenu(state);
        paused = true;
    }

    public void Resume()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        PlayMode.ChangeGameMode(lastMode);
        if(lastMode == PlayMode.GameMode.Conversation)
        {
            explore.SetActive(true);
            conversation.SetActive(true);
            conversation.GetComponent<Animator>().SetBool("Activated", true);
        }
        else if(lastMode == PlayMode.GameMode.Explore)
        {
            explore.SetActive(true);
        }


        menuCanvas.SetActive(false);
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangePanel(GameObject panel)
    {
        activePanel.SetActive(false);
        activePanel = panel;
        panel.SetActive(true);
    }

    public void Save()
    {
        gameObject.GetComponent<DataManager>().SaveData();
    }

    public void Load()
    {
        gameObject.GetComponent<DataManager>().LoadData();
    }
}
