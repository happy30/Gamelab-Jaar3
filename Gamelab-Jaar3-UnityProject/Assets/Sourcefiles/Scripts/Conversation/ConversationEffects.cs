//By Jordi

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

	public void PickUp(string name)
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
