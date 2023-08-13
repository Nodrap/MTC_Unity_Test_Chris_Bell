using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject gameHUD;
	public GameObject gameOver;

	public TMP_Text gameScore;
	public TMP_Text gameLevel;
	public TMP_Text finalScore;

	public ArenaManager arenaManager;
	public GameData gameData;

	public enum eUIState
	{
		E_MAIN_MENU,
		E_GAME,
		E_GAME_OVER,
	}

	public void ShowScreen(eUIState screen)
	{
		mainMenu.SetActive(screen == eUIState.E_MAIN_MENU);
		gameHUD.SetActive(screen == eUIState.E_GAME);
		gameOver.SetActive(screen == eUIState.E_GAME_OVER);
		
		arenaManager.ShowArena(screen == eUIState.E_GAME || screen == eUIState.E_GAME_OVER);
	}

	public void SetScore(float score)
	{
		gameScore.text = string.Format(gameData.scoreFormat, score);
	}

	public void SetLevel(float level)
	{
		gameLevel.text = string.Format(gameData.levelFormat, level);
	}

	public void SetFinalScore(float score)
	{
		finalScore.text = string.Format(gameData.finalScoreFormat, score);
	}
}
