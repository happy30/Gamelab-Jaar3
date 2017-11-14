//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IconPuzzle : MonoBehaviour
{
    public GameObject[] asterixes;
    public int inputs;

    public string code;
    public string correctCode;
    public GameObject completeScreen;
    public GameObject computer;
    public Texture completedTexture;

    public void AddToString(string number)
    {
        code += number;
        asterixes[inputs].SetActive(true);
        inputs++;

        if(inputs > 4)
        {
            print("CheckingCode");
            CheckCode();
        }
    }

    public void CheckCode()
    {
        print(code);
        print (correctCode);

        if(code == correctCode)
        {
            CompletePuzzle();
        }
        else
        {
            ResetPuzzle();
        }
    }

    public void ResetPuzzle()
    {
        code = "";
        inputs = 0;
        foreach(GameObject ast in asterixes)
        {
            ast.SetActive(false);
        }
    }

    public void CompletePuzzle()
    {
        completeScreen.SetActive(true);
        GameObject.Find("IconComputer").GetComponent<Renderer>().material.mainTexture = completedTexture;
        GameObject.Find("SceneSettings").GetComponent<ClientsCellData>().iconPuzzleCompleted = true;
        GameObject.Find("SceneSettings").GetComponent<ClientsCellData>().interactions[0].Trigger(true);
        computer.GetComponent<Collider>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
