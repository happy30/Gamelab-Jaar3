//By Jordi

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

    public bool activated;

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
    CameraBehaviour cam;

    ConversationContainer cc;

    public bool lineDone;
    public bool choicesShown;

    public Conversation currentConversation;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
        cUI = GameObject.Find("Canvas").GetComponent<ConversationUI>();
        interact = GetComponent<Interact>();
        effects = GameObject.Find("GameManager").GetComponent<ConversationEffects>();
        cam = Camera.main.GetComponent<CameraBehaviour>();
    }

    void Start()
    {
        cc = GameObject.Find("SceneSettings").GetComponent<ConversationLoader1>().cc;
    }

    void Update()
    {
        if (activated)
        {
            WaitForLineToBeFullyDisplayed();
        }
        
        if(lineDone)
        {
            if(CheckIfChoices() && !choicesShown)
            {
                ShowChoices();
                choicesShown = true;
            }
            else if (!choicesShown)
            {
                WaitForInputToGetToNextLine();
            }
            
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
            lineDone = true;
        }
    }


    bool CheckIfChoices()
    {
        if(currentConversation.lines[currentText].choice.choice1 != "")
        {
            return true;
        }
        return false;
    }


    void ShowChoices()
    {
        Choice choices = currentConversation.lines[currentText].choice;

        string choice1Text;
        string choice2Text;
        string choice3Text;

        if (choices.choice1 != "")
        {
            choice1Text = choices.choice1;
            cUI.FillChoices(0, choice1Text);
        }

        if (choices.choice2 != "")
        {
            choice2Text = choices.choice2;
            cUI.FillChoices(1, choice2Text);
        }

        if (choices.choice3 != "")
        {
            choice3Text = choices.choice3;
            cUI.FillChoices(2, choice3Text);
        }
    }

    //When the sentence is formed, a click will progress the conversation to the next line.
    void WaitForInputToGetToNextLine()
    {
        if (displayLine == fullLine)
        {
            cUI.ProgressArrowBox.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentText < currentConversation.lines.Length - 1)
                {
                    currentText++;
                    NextLine();
                }
                else
                {
                    cUI.ProgressArrowBox.SetActive(false);
                    CloseConversation();
                }
            }
        }
    }


    //Initializing a conversation. When the player interacts with the object of interest:
    public void ActivateConversation(string interactCode, Interact.InteractType interactType)
    {
        print("activating");
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
        if (PlayMode.gameMode == PlayMode.GameMode.Conversation)
        {
            cam.SetCameraOffset();
        }

        //currentConversation.lines[currentText].expression.portraitExpression.ToString();
        //Here starts the first line of conversation
        NextLine();
            

    }

    //Bye bye.
    void CloseConversation()
    {
        lineDone = false;
        activated = false;
        choicesShown = false;
        currentText = 0;
        cUI.diaBoxCol.box.color = Color.black;
        interact.Trigger(false);
    }

    //Send the info to the UI
    void SendToUI()
    {
        cUI.DisplayConversationUI(
                    actor,
                    displayLine);
    }

    

    //Get the next line in our conversation
    void NextLine()
    {
        //Reset the lien progress
        lineDone = false;
        choicesShown = false;
        currentChar = 0;
        displayLine = "";

        //Get the new next line
        fullLine = currentConversation.lines[currentText].text;

        //Get the new actor of that line
        actor = currentConversation.lines[currentText].actors.actor.ToString();

        //Get the color of that actor
        cUI.RefreshColor(currentConversation.lines[currentText].actors.actor);

        //Reset the progressbox
        cUI.ProgressArrowBox.SetActive(false);

        //Reset the choices;
        cUI.RefreshChoices();

        //Play Audio
        if(currentConversation.lines[currentText].expression.voiceActing)
        {
            cUI.PlayVoice(actor + "/" + "SFX_" + actor + "_" + currentConversation.lines[currentText].expression.portraitExpression);
            print(actor + "/" + "SFX_" + actor + "_" + currentConversation.lines[currentText].expression.portraitExpression);
        }

        if(type == Interact.InteractType.Conversation)
        {
            if (SetCameraPosition())
            {
                cUI.port.newPortrait = currentConversation.lines[currentText].expression.portraitExpression.ToString();
                cUI.port.newActor = actor;
                cUI.port.waiting = true;
            }
            else
            {
                cUI.UpdatePortrait(actor, currentConversation.lines[currentText].expression.portraitExpression.ToString());
            }
        }
        else if (type == Interact.InteractType.Examine)
        {
            cUI.RemovePortrait();
        }
        

        CheckEffect();
        activated = true;
    }



    bool SetCameraPosition()
    {
        switch (currentConversation.lines[currentText].cameraPosition.move)
        {
            case Movement.Move.Left:
                cam.conversationYRotation = -30;
                break;

            case Movement.Move.Right:
                cam.conversationYRotation = 30;
                break;

            case Movement.Move.Middle:
                cam.conversationYRotation = 0;
                break;
        }

        cam.SetCameraRotation();

        if (currentText > 0)
        {
            if(currentConversation.lines[currentText].cameraPosition.move != currentConversation.lines[currentText - 1].cameraPosition.move)
            {
                if(currentConversation.lines[currentText - 1].cameraPosition.move == Movement.Move.Left)
                {
                    cUI.RefreshPortrait(true);
                    return true;
 
                }
                else if(currentConversation.lines[currentText - 1].cameraPosition.move == Movement.Move.Right)
                {
                    cUI.RefreshPortrait(false);
                    return true;

                }
                else
                {
                    if(currentConversation.lines[currentText].cameraPosition.move == Movement.Move.Left)
                    {
                        cUI.RefreshPortrait(false);
                        return true;

                    }
                    else
                    {
                        cUI.RefreshPortrait(true);
                        return true;
                    }
                }
            }

        }
        return false;
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

    //If the last effect had black, and the next one hasn't then fade out.
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
