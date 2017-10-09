//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    private bool paused;
    public GameObject pausePanel;
    public GameObject activePanel;

    void Start()
    {
        activePanel = pausePanel;
    }
	
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                Time.timeScale = 1;
                activePanel.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                activePanel.SetActive(true);
                paused = true;
            }
        }
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
