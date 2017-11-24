using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    public List<Item> ccItems = new List<Item>();
    public List<Item> morItems = new List<Item>();
    public List<Item> dirItems = new List<Item>();


    public List<Combination> ccCombos = new List<Combination>();
    public List<Combination> morCombos = new List<Combination>();
    public List<Combination> dirCombos = new List<Combination>();


    public bool CheckCombination(int i1, int i2)
    {
        List<Combination> combs = new List<Combination>();
        List<Item> invs = new List<Item>();

        switch (PlayMode.escapeRoom)
        {
            case PlayMode.EscapeRoom.ClientsCell:
                combs = ccCombos;
                invs = ccItems;
                break;

            case PlayMode.EscapeRoom.Morgue:
                combs = morCombos;
                invs = morItems;
                break;

            case PlayMode.EscapeRoom.DirectorsOffice:
                combs = dirCombos;
                invs = dirItems;
                break;

        }

        foreach (Combination combo in combs)
        {
            if(combo.input1 == i1 && combo.input2 == i2 || combo.input1 == i2 && combo.input2 == i1)
            {
                foreach (Item item in inventory.ToArray())
                {
                    if(item.id == i1 || item.id == i2)
                    {
                        inventory.Remove(item);
                    }
                }
                inventory.Add(invs[combo.output]);
                return true;
            }
        }
        return false;
    }

    public void RemoveHeldItem(Item item)
    {
        inventory.Remove(item);
    
       HeldItem heldItem = GameObject.Find("HeldItemController").GetComponent<HeldItem>();


       if (heldItem.spawnedPrefab != null)
       {
            heldItem.itemName.text = "";
            heldItem.CheckIfItemExists();
            Destroy(heldItem.spawnedPrefab);
       }
    }
}
