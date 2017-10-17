//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour
{
    public GameObject prefab;
    public Transform itemPlace;
    float rotateTo;
    Vector3 currentRot;

    int numObjects;
    int selectedItem;

    int firstInput;
    int selectedItems;

    public InventoryManager inventoryManager;

    public List<GameObject> spawnedObjects = new List<GameObject>();

    float radius = 5;

    float timer;

    public Text itemName;



    void Update()
    {
        currentRot = new Vector3(0, Mathf.LerpAngle(currentRot.y, rotateTo, 7f * Time.deltaTime), 0);
        itemPlace.GetComponent<RectTransform>().localEulerAngles = currentRot;

        timer += Time.deltaTime;
    }


    public void ShowItems()
    {
        selectedItem = 0;
        foreach(GameObject obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
        numObjects = inventoryManager.inventory.Count;

        for (int i = 0; i < numObjects; i++)
        {
            float theta = i * 2 * Mathf.PI / numObjects;
            float x = Mathf.Sin(theta) * radius;
            float y = Mathf.Cos(theta) * radius;

            GameObject ob = (GameObject)Instantiate(inventoryManager.inventory[i].Prefab);
            ob.transform.parent = itemPlace;
            ob.transform.localPosition = new Vector3(x * 120, 0, y * 120);

            spawnedObjects.Add(ob);
        }

        rotateTo = 0;
        itemPlace.localEulerAngles = new Vector3(0, 0, 0);

        if(inventoryManager.inventory.Count > 0)
        {
            itemName.text = inventoryManager.inventory[selectedItem].name;
        }
        
    }




    public void RotateInventory(bool right)
    {
       
        if(timer > 0.65f)
        {
            if (right)
            {
                rotateTo = (360 / numObjects) + itemPlace.gameObject.GetComponent<RectTransform>().localEulerAngles.y;

                if (selectedItem > 0)
                {
                    selectedItem--;
                }
                else
                {
                    selectedItem = inventoryManager.inventory.Count - 1;
                }


            }
            else
            {
                rotateTo = -(360 / numObjects) + itemPlace.gameObject.GetComponent<RectTransform>().localEulerAngles.y;

                if (selectedItem < inventoryManager.inventory.Count - 1)
                {
                    selectedItem++;
                }
                else
                {
                    selectedItem = 0;
                }

                

            }
            itemName.text = inventoryManager.inventory[selectedItem].name;
            timer = 0;
        }

        //itemPlace.gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, itemPlace.gameObject.GetComponent<RectTransform>().localEulerAngles.y + rotateAmount, 0);
    }

    public void Combine()
    {
        if(selectedItems == 0)
        {
            selectedItems++;
            firstInput = inventoryManager.inventory[selectedItem].id;
            spawnedObjects[selectedItem].GetComponent<RotateItem>().selectSprite.SetActive(true);
        }
        else
        {
            if(inventoryManager.CheckCombination(firstInput, inventoryManager.inventory[selectedItem].id))
            {
                ShowItems();
            }
            else
            {
                spawnedObjects[selectedItem].GetComponent<RotateItem>().selectSprite.SetActive(false);
                selectedItems = 0;
            }
        }
    }
}

