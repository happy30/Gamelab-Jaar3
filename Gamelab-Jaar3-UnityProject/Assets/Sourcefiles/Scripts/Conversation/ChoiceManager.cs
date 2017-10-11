//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public enum cursorState
    {
        first,
        second,
        third
    };

    cursorState state;

    public GameObject[] choices = new GameObject[3];
    public Text[] choiceTexts = new Text[3];

    public GameObject choiceArrow;

    bool choicesShown;
    bool choicesActivated;
    float arrowLoc;
    float arrowSpeed;
    float timer;

    ConversationStats stats;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
    }

    void Start()
    {
        arrowSpeed = 5;
    }


    public void FillChoices(int choice, string text)
    {
        choices[choice].SetActive(true);
        choiceTexts[choice].text = text;
        choiceTexts[choice].color = Color.white;
        choices[choice].GetComponent<Image>().color = stats.interactedObject.gameObject.GetComponent<ConversationController>().cUI.diaBoxCol.newColor;
    }

    public void RefreshChoices()
    {
        choiceArrow.SetActive(false);
        choicesActivated = false;
        choicesShown = false;

        foreach(GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    public void ActivateChoices()
    {
        state = cursorState.first;
        choiceArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(choiceArrow.GetComponent<RectTransform>().anchoredPosition.x, 200);
        choicesShown = true;
    }

    void Update()
    {
        if(choicesShown)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                choicesActivated = true;
            }
        }

        if(choicesActivated)
        {
            choiceArrow.SetActive(true);
            choiceArrow.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(
                choiceArrow.GetComponent<RectTransform>().anchoredPosition, 
                new Vector2(choiceArrow.GetComponent<RectTransform>().anchoredPosition.x, arrowLoc), arrowSpeed * Time.deltaTime);

            if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if((int)state > 0f)
                {
                    state--;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if ((int)state < activeChoices())
                {
                    state++;
                }
            }

            foreach (GameObject choice in choices)
            {
                if (choice.activeSelf)
                {
                    //choice.GetComponent<Image>().color = Color.black;
                    choice.GetComponent<Image>().color = stats.interactedObject.gameObject.GetComponent<ConversationController>().cUI.diaBoxCol.newColor;
                }
            }

            foreach (Text text in choiceTexts)
            {
                if (text.gameObject.activeSelf)
                {
                    text.color = Color.white;
                }
            }

            switch (state)
            {
                case cursorState.first:
                    choices[0].GetComponent<Image>().color = Color.white;
                    choiceTexts[0].color = Color.black;
                    arrowLoc = 200;
                    break;

                case cursorState.second:
                    choices[1].GetComponent<Image>().color = Color.white;
                    choiceTexts[1].color = Color.black;
                    arrowLoc = 100;
                    break;

                case cursorState.third:
                    choices[2].GetComponent<Image>().color = Color.white;
                    choiceTexts[2].color = Color.black;
                    arrowLoc = 0;
                    break;
            }

            
            choiceArrow.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(
                choiceArrow.GetComponent<RectTransform>().anchoredPosition,
                new Vector2(choiceArrow.GetComponent<RectTransform>().anchoredPosition.x, arrowLoc), arrowSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            if(Input.GetButtonDown("Fire1") && timer > 0.3f)
            {
                //stats.interactedObject.Trigger(false);

                timer = 0;

                switch (state)
                {
                    case cursorState.first:
                        stats.interactedObject.interactionCodeName = stats.interactedObject.gameObject.GetComponent<ConversationController>().currentConversation.lines[stats.interactedObject.gameObject.GetComponent<ConversationController>().currentText].choice.Destination1;
                        break;

                    case cursorState.second:
                        stats.interactedObject.interactionCodeName = stats.interactedObject.gameObject.GetComponent<ConversationController>().currentConversation.lines[stats.interactedObject.gameObject.GetComponent<ConversationController>().currentText].choice.Destination2;
                        break;

                    case cursorState.third:
                        stats.interactedObject.interactionCodeName = stats.interactedObject.gameObject.GetComponent<ConversationController>().currentConversation.lines[stats.interactedObject.gameObject.GetComponent<ConversationController>().currentText].choice.Destination3;
                        break;

                }

                stats.interactedObject.Trigger(true);
                
            }
        }
    }

    int activeChoices()
    {
        if(choices[2].activeSelf)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

}
