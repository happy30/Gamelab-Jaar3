//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuCanvas;
    public PlayMode.GameMode lastMode;



    private bool paused;
    public GameObject activePanel;
	
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Pause()
    {
        lastMode = PlayMode.gameMode;
        PlayMode.gameMode = PlayMode.GameMode.Menu;
        menuCanvas.SetActive(true);
        paused = true;
    }

    public void Resume()
    {
        
        PlayMode.gameMode = lastMode;
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
