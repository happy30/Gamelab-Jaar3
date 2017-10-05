using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationController : MonoBehaviour
{

    public ConversationUI cUI;
    Interact interact;

    string code;
    Interact.InteractType type;

    bool activated;

    [HideInInspector]
    public string displayLine;
    string fullLine;

    [HideInInspector]
    public string actor;

    int currentChar;

    [HideInInspector]
    public int currentText;

    ConversationStats stats;
    ConversationEffects effects;
    public const string path = "Conversations";
    ConversationContainer cc;


    public Conversation currentConversation;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
        cUI = GameObject.Find("Canvas").GetComponent<ConversationUI>();
        interact = GetComponent<Interact>();
        effects = GameObject.Find("GameManager").GetComponent<ConversationEffects>();
    }

    void Start()
    {
        cc = ConversationContainer.Load(path);
    }

    void Update()
    {
        if (activated)
        {
            WaitForLineToBeFullyDisplayed();
        }
        else
        {
            WaitForInputToGetToNextLine();
        }

    }


    //Invoke a method to display characters one at the time.
    void WaitForLineToBeFullyDisplayed()
    {
        if (displayLine != fullLine)
        {
            //ui.sentenceFinishedArrow.SetActive(false);
            if (!IsInvoking("NextChar"))
            {
                Invoke("NextChar", stats.slowTextSpeed);
            }
            SendToUI();
        }
        else
        {
            SendToUI();
            activated = false;
        }
    }

    //When the sentence is formed, a click will progress the conversation to the next line.
    void WaitForInputToGetToNextLine()
    {
        if (displayLine == fullLine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentText < currentConversation.lines.Length - 1)
                {
                    currentText++;
                    NextLine();
                }
                else
                {
                    CloseConversation();
                }
            }
        }
    }


    //Initializing a conversation. When the player interacts with the object of interest:
    public void ActivateConversation(string interactCode, Interact.InteractType interactType)
    {
        if (PlayMode.gameMode == PlayMode.GameMode.Conversation)
        {
            code = interactCode;
            type = interactType;
            
            currentText = 0;

            //Get the right conversation from the XML
            for (int i = 0; i < cc.interactions.Count; i++)
            {
                if (cc.interactions[i].interactionCodeName == code)
                {
                    currentConversation = cc.interactions[i];
                    break;
                }
            }

            //Here starts the first line of conversation
            NextLine();
            
        }
    }

    //Bye bye.
    void CloseConversation()
    {
        currentText = 0;
        interact.Trigger(false);
    }

    //Send the info to the UI
    void SendToUI()
    {
        cUI.DisplayConversationUI(
                    actor,
                    displayLine,
                    currentConversation.lines[currentText].cameraPosition.move.ToString(),
                    currentConversation.lines[currentText].expression.portraitExpression.ToString());
    }

    //Get the next line in our conversation
    void NextLine()
    {
        print(currentText);
        currentChar = 0;
        displayLine = "";
        fullLine = currentConversation.lines[currentText].text;
        actor = currentConversation.lines[currentText].actors.actor.ToString();
        CheckEffect();
        activated = true;
    }

    //Get the next character in our line
    void NextChar()
    {
        if (currentChar < fullLine.Length)
        {
            displayLine += fullLine[currentChar];
            currentChar++;
        }
    }

    //Check if there is an effect for this line. If so, the ConversationEffects.cs will handle them.
    void CheckEffect()
    {
        if(CheckForFadeOut())
        {
            effects.FadeOutBlack();
        }


        if(currentConversation.lines[currentText].additionalEffect != "")
        {
            if(currentConversation.lines[currentText].effectParameter == "")
            {
               effects.SendMessage(currentConversation.lines[currentText].additionalEffect);
            }
            else
            {
                effects.SendMessage(currentConversation.lines[currentText].additionalEffect, currentConversation.lines[currentText].effectParameter);
            }
            
        }

    }

    bool CheckForFadeOut()
    {
        if(currentConversation.lines[currentText].additionalEffect != "Black" && currentText > 0)
        {
            if(currentConversation.lines[currentText - 1].additionalEffect == "Black")
            {
                return true;
            }
        }
        return false;
    }
}
