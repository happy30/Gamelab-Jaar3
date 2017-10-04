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
    public const string path = "Conversations";
    ConversationContainer cc;


    public Conversation currentConversation;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
        cUI = GameObject.Find("Canvas").GetComponent<ConversationUI>();
        interact = GetComponent<Interact>();
    }

    void Start()
    {
        cc = ConversationContainer.Load(path);
    }


    public void ActivateConversation(string interactCode, Interact.InteractType interactType)
    {
        if (PlayMode.gameMode == PlayMode.GameMode.Conversation)
        {
            code = interactCode;
            type = interactType;
            
            currentText = 0;
            

            for (int i = 0; i < cc.interactions.Count; i++)
            {
                if (cc.interactions[i].interactionCodeName == code)
                {
                    currentConversation = cc.interactions[i];
                    break;
                }
            }

            NextLine();
            
        }
    }

    void CloseConversation()
    {
        currentText = 0;
        interact.Trigger(false);
    }

    void Update()
    {
        if (activated)
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
        else
        {
            if(displayLine == fullLine)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    if(currentText < currentConversation.lines.Length -1)
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
       
    }



    void SendToUI()
    {
        cUI.DisplayConversationUI(
                    actor,
                    displayLine,
                    currentConversation.lines[currentText].cameraPosition.move.ToString(),
                    currentConversation.lines[currentText].expression.portraitExpression.ToString());
    }

    void NextLine()
    {
        print(currentText);
        currentChar = 0;
        displayLine = "";
        fullLine = currentConversation.lines[currentText].text;
        actor = currentConversation.lines[currentText].actors.actor.ToString();
        activated = true;
    }

    void NextChar()
    {
        if (currentChar < fullLine.Length)
        {
            displayLine += fullLine[currentChar];
            currentChar++;
        }
    }
}
