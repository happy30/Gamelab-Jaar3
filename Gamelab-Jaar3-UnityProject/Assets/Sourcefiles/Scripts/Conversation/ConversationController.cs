﻿//By Jordi

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
    ExploreStats exploreStats;

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

	public ConversationContainer cc;

	public bool lineDone;
	public bool choicesShown;

    [HideInInspector]
    public bool SetInactiveAfterConversaton;

    [HideInInspector]
    public bool EnterPuzzleAfterConversation;

    [HideInInspector]
    public bool ClosePuzzleAfterConversation;

    [HideInInspector]
    public bool ShowInfoTextAfterConversation;

    public bool textCanBeSkipped;
    bool skippingText;

	public Conversation currentConversation;

    float slowTextSpeed;
    float fastTextSpeed;

    public bool slowText;

	void Awake()
	{
		stats = GameObject.Find("GameManager").GetComponent<ConversationStats>();
		cUI = GameObject.Find("HUDCanvas").GetComponent<ConversationUI>();
		interact = GetComponent<Interact>();
		effects = GameObject.Find("GameManager").GetComponent<ConversationEffects>();
		cam = Camera.main.GetComponent<CameraBehaviour>();

		if(GetComponent<Interact>().interactType != Interact.InteractType.Use)
		{
			cc = GameObject.Find("SceneSettings").GetComponent<ConversationLoader1>().LoadConversation(GetComponent<Interact>().xmlName);
		}
	}

    void Start()
    {
        if(slowText)
        {
            Debug.Log("slow text");
            slowTextSpeed = 0.1f;
            fastTextSpeed = 0.1f;
        }
        else
        {
            slowTextSpeed = stats.slowTextSpeed;
            fastTextSpeed = stats.fastTextSpeed;
        }
        
    }


	void Update()
	{
		if(PlayMode.gameMode == PlayMode.GameMode.Conversation)
		{
			if (activated)
			{
				WaitForLineToBeFullyDisplayed();
			}

			if (lineDone)
			{
				if (CheckIfChoices() && !choicesShown)
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
	}


	//Invoke a method to display characters one at the time.
	void WaitForLineToBeFullyDisplayed()
	{
		//if (displayLine != fullLine)
        if(currentChar < fullLine.Length)
		{
			if(!skippingText)
            {
                if (!IsInvoking("NextChar"))
                {
                    Invoke("NextChar", slowTextSpeed);
                }
            }
            else
            {
                if (!IsInvoking("Next10Char"))
                {
                    Invoke("Next10Char", fastTextSpeed);
                }
            }

            if(Input.GetButtonDown("Fire1") && textCanBeSkipped & displayLine.Length > 2)
            {
                skippingText = true;
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
		if(currentConversation.lines[currentText].choices.Length > 0)
		{
			return true;
		}
		return false;
	}


	void ShowChoices()
	{
		Choices[] choices = currentConversation.lines[currentText].choices;


        string choice1Text;
        string choice2Text;
        string choice3Text;

		if (choices.Length > 0)
		{
			choice1Text = choices[0].choice;
			cUI.FillChoices(0, choice1Text);
		}

		if (choices.Length > 1)
		{
			choice2Text = choices[1].choice;
			cUI.FillChoices(1, choice2Text);
		}

		if (choices.Length > 2)
		{
			choice3Text = choices[2].choice;
			cUI.FillChoices(2, choice3Text);
		}
	}

	//When the sentence is formed, a click will progress the conversation to the next line.
	void WaitForInputToGetToNextLine()
	{
		if (currentChar <= fullLine.Length && displayLine != "")
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
		code = interactCode;
		type = interactType;
			
		currentText = 0;

		//Get the right conversation from the XML
		for (int i = 0; i < cc.interactions.Count; i++)
		{
			if (cc.interactions[i].icn == code)
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
	public void CloseConversation()
	{
		lineDone = false;
		activated = false;
		choicesShown = false;
		currentText = 0;
		cUI.diaBoxCol.box.color = Color.black;

        stats.AddInteraction(currentConversation.icn);

        cUI.RemovePortrait();

		interact.Trigger(false);
		if(SetInactiveAfterConversaton)
		{
			GameObject.Find("HUDCanvas").GetComponent<MenuUI>().Pause(MenuManager.MenuState.Items, true);
			gameObject.SetActive(false);
		}

		if(EnterPuzzleAfterConversation)
		{
			GameObject.Find("SceneSettings").GetComponent<PuzzleRoomData>().StartPuzzle();
		}

		if(ClosePuzzleAfterConversation)
		{
			GameObject.Find("SceneSettings").GetComponent<PuzzleRoomData>().EndPuzzle();
		}

        if(ShowInfoTextAfterConversation)
        {
            GameObject.Find("HUDCanvas").GetComponent<TextFade>().anim.SetTrigger("Activate");
        }

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
        skippingText = false;
        lineDone = false;
		choicesShown = false;
		currentChar = 0;
		displayLine = "";
        

		//Check for the effects to happen during conversation
		CheckEffect();

        //Check if we can skip this line
        CheckIfTextSkippable();

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
				if(currentText == 0 && currentConversation.lines[currentText].actors.actor == Actors.Actor.Mind)
				{
					cUI.RemovePortrait();
				}
				else
				{
					cUI.UpdatePortrait(actor, currentConversation.lines[currentText].expression.portraitExpression.ToString());
				}
			}
		}
		else if (type == Interact.InteractType.Examine)
		{
			cUI.RemovePortrait();
		}
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
					if(currentConversation.lines[currentText].actors.actor == Actors.Actor.Dougal)
					{
						print("Remove");
						cUI.RemovePortrait();
					}
					else
					{
						print("Refresh");
						cUI.RefreshPortrait(true);
					}
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
						if (currentConversation.lines[currentText].actors.actor == Actors.Actor.Dougal)
						{
							print("Remove");
							cUI.RemovePortrait();
						}
						else
						{
							print("Refresh");
							cUI.RefreshPortrait(true);
						}
						return true;
					}
				}
			}

		}
		return false;
	}

    public void CheckIfTextSkippable()
    {
        foreach (Interaction inter in stats.interactions)
        {
            if (inter.interactionCodeName == currentConversation.icn)
            {
                textCanBeSkipped = true;
                break;
            }
            textCanBeSkipped = false;
        }
    }

	//Get the next character in our line
	void NextChar()
	{
		if (currentChar < fullLine.Length)
		{
            if(fullLine[currentChar].ToString() == "|")
            {
                slowTextSpeed = 0.15f;
            }
            else
            {
                if(!slowText)
                {
                    slowTextSpeed = stats.slowTextSpeed;
                }
                
                displayLine += fullLine[currentChar];
            }
			currentChar++;
		}
	}

    void Next10Char()
    {
        for(int i = 0; i < 10; i++)
        {
            if (currentChar < fullLine.Length)
            {
                if (fullLine[currentChar].ToString() == "|")
                {
                    slowTextSpeed = 0.5f;
                }
                else
                {
                    slowTextSpeed = stats.slowTextSpeed;
                    displayLine += fullLine[currentChar];
                }
                currentChar++;
            }
        }
    }


    //Check if there is an effect for this line. If so, the ConversationEffects.cs will handle them.
    void CheckEffect()
	{
		if(CheckForFadeOut())
		{
			effects.FadeOutBlack();
            print("FadeOutBlack");
		}


        if(currentConversation.lines[currentText].effects.Length > 0)
        {
            Effect[] fx;
            fx = currentConversation.lines[currentText].effects;

            for (int i = 0; i < fx.Length; i++)
            {
                if (fx.Length > 0)
                {
                    if (fx[i].effect == Effect.AdditionalEffect.CheckItem) //additionalEffect == "CheckItem")
                    {
                        effects.CheckItem(fx[i].parameter[i], fx[i].parameter[1]);
                    }
                    else if (fx[i].effect != Effect.AdditionalEffect.None)
                    {
                        if (fx[i].parameter.Length > 0)
                        {
                            if ((fx[i].parameter.Length > 1))
                            {
                                effects.SendMessage(fx[i].effect.ToString(), fx[i].parameter[1]);
                            }
                            else
                            {
                                effects.SendMessage(fx[i].effect.ToString(), fx[i].parameter[0]);
                            }

                        }
                        else
                        {

                            effects.SendMessage(fx[i].effect.ToString());
                        }

                    }
                }
            }
        }
	}

	//If the last effect had black, and the next one hasn't then fade out.
	bool CheckForFadeOut()
	{
		if(currentText == 1)
		{
			return true;
		}




        Effect[] fx;
        fx = currentConversation.lines[currentText].effects;

        print(fx.Length);

        if(fx.Length < 1)
        {
            return true;
        }

        for(int i = 0; i < fx.Length; i++)
        {
            if (fx.Length > 0)
            {
                if (fx[i].effect != Effect.AdditionalEffect.Black && currentText > 0)
                {
                    if(currentConversation.lines[currentText - 1].effects.Length > 0)
                    {
                        if (currentConversation.lines[currentText - 1].effects[0].effect == Effect.AdditionalEffect.Black)
                        {
                            return true;
                        }
                    }
                }
            }
        }
		return false;
	}
}
