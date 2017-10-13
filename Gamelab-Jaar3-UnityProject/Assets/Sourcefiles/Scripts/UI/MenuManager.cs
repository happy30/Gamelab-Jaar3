using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public enum MenuState
    {
        Flow,
        Items,
        System
    };

    public MenuState menuState;

    public GameObject flowView;
    public GameObject itemsView;
    public GameObject systemView;


	
    public void SetMenu(MenuState state)
    {
        switch (state)
        {
            case MenuState.Flow:
                flowView.SetActive(true);
                itemsView.SetActive(false);
                systemView.SetActive(false);
                break;

            case MenuState.Items:
                itemsView.SetActive(true);
                flowView.SetActive(false);
                systemView.SetActive(false);
                break;

            case MenuState.System:
                systemView.SetActive(true);
                flowView.SetActive(false);
                itemsView.SetActive(false);
                break;
        }
    }

    public void ItemButton()
    {
        SetMenu(MenuState.Items);
    }

    public void FlowButton()
    {
        SetMenu(MenuState.Flow);
    }

    public void SystemButton()
    {
        SetMenu(MenuState.System);
    }



	// Update is called once per frame
	void Update ()
    {
		
	}
}
