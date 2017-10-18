using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldItem : MonoBehaviour
{
    public GameObject prefab;
    public string text;
    public Text itemName;

    public InventoryManager inv;

    GameObject spawnedPrefab;


    void Awake()
    {
        inv = GameObject.Find("GameManager").GetComponent<InventoryManager>();
    }

    public void ChangeItem(int id)
    {
        if(spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
        
        prefab = inv.inventory[id].Prefab;
        text = inv.inventory[id].name;
        itemName.text = text;
        spawnedPrefab = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
        spawnedPrefab.transform.SetParent(transform);
        spawnedPrefab.transform.localPosition = Vector3.zero;
        spawnedPrefab.transform.localScale = inv.inventory[id].Prefab.transform.localScale;
    }
}
