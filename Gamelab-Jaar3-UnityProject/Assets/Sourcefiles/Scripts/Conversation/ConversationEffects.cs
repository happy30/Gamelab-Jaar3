﻿//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationEffects : MonoBehaviour
{

	public void Strobe()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().Strobe();
	}

	public void Black()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().Black();
	}

	public void FadeOutBlack()
	{
		GameObject.Find("Canvas").GetComponent<ConversationUI>().FadeOutBlack();
	}

    public void CheckItem(string name, string newCode)
    {
        print(name + newCode);

        InventoryManager inv = GetComponent<InventoryManager>();
        foreach (Item item in inv.inventory)
        {
            if(item.name == name)
            {
                GetComponent<ConversationStats>().interactedObject.interactionCodeName = newCode;
                GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().CloseConversation();
                GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().ActivateConversation(newCode, GetComponent<ConversationStats>().interactedObject.interactType);
                print("CHANGEING CONVERSAIOTN");

            }
        }
    }

    public void PickUp(string name)
    {
        GetItem(name);
        //GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject.Trigger(false);
        GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject.gameObject.GetComponent<ConversationController>().SetInactiveAfterConversaton = true;
    }

	public void GetItem(string name)
	{
		InventoryManager inv = GetComponent<InventoryManager>();


		switch (PlayMode.escapeRoom)
		{
			case PlayMode.EscapeRoom.ClientsCell:

				foreach(Item item in inv.ccItems)
				{
					if(item.name == name)
					{
						inv.inventory.Add(item);
					}
				}
				break;
		}
	}


}
