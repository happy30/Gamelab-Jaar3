//PlayMode by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
	public enum GamePart
	{
		FreeTime,
		Escape,
		Story
	}

	public enum GameMode
	{
		Explore,
		Conversation,
		Puzzle,
		Menu
	}

	public enum EscapeRoom
	{
		ClientsCell,
		Morgue,
		DirectorsOffice
	};

	public static GamePart gamePart;
	public static GameMode gameMode;
	public static EscapeRoom escapeRoom;


	public static void ChangeGameMode(GameMode mode)
	{
		gameMode = mode;
		print(mode);
	}

	public static void ChangeGamePart(GamePart part)
	{
		gamePart = part;
	}
}
