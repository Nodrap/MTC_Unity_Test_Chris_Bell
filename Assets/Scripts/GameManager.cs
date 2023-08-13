using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager sInst;
	public enum eGameState
	{
		E_STARTUP,
		E_MAIN_MENU,
		E_INTRO,
		E_PLAYING,
		E_GAME_OVER,
	}

	public enum eScoreZone
	{
		E_PERFECT,
		E_GOOD,
		E_LIMIT,
		FAIL
	}

	public UIManager uiManager;
	public ArenaManager arenaManager;
	public CameraController cameraController;
	public GameObject levelOrigin;
	public GameData gameData;

	[Header("RUNTIME")]
	public eGameState gameState;
	public float stateTime;

	public PlayerController currentPlayer;

	public float score;

	public float currentLevel;

	private void Awake()
	{
		Debug.Assert(sInst == null, "Already have instance of singleton");

		sInst = this;
	}

	void Start()
	{
		SetState(eGameState.E_STARTUP);
	}

	public void OnStart()
	{
		SetState(eGameState.E_INTRO);
	}

	private void SetState(eGameState newState)
	{
		gameState = newState;
		stateTime = 0;

		switch (gameState)
		{
		case eGameState.E_STARTUP:
			SetState(eGameState.E_MAIN_MENU);
			break;

		case eGameState.E_MAIN_MENU:
			arenaManager.Reset();
			cameraController.SetTrackingPoint(levelOrigin.transform.position);
			uiManager.ShowScreen(UIManager.eUIState.E_MAIN_MENU);

			cameraController.SetTrackingPoint(levelOrigin.transform.position, true);
			break;

		case eGameState.E_INTRO:
			score = 0;
			currentLevel = 1;
			UpdateUI();
			uiManager.ShowScreen(UIManager.eUIState.E_GAME);
			break;

		case eGameState.E_PLAYING:
			StartALevel();
			break;

		case eGameState.E_GAME_OVER:
			uiManager.SetFinalScore(score);
			uiManager.ShowScreen(UIManager.eUIState.E_GAME_OVER);
			break;

		default:
			break;
		}

		// No code here. Allows for chaining of states
	}

	private void StartALevel()
	{
		currentPlayer = arenaManager.AddLevel();

		currentPlayer.OnPlayerStopped += OnPlayerStopped;

		cameraController.SetTrackingPoint(currentPlayer.transform.position);
	}

	// Award score or end game
	private void OnPlayerStopped(eScoreZone newScore)
	{
		currentPlayer.OnPlayerStopped -= OnPlayerStopped;

		float scored = 0;

		// Score or end
		switch (newScore)
		{
		case eScoreZone.E_PERFECT:
			scored = gameData.scorePerfect;
			break;

		case eScoreZone.E_GOOD:
			scored = gameData.scoreGood;
			break;

		case eScoreZone.E_LIMIT:
			scored = gameData.scoreLimit;
			break;

		case eScoreZone.FAIL:
			SetState(eGameState.E_GAME_OVER);
			return;
		}

		score += scored;

		currentPlayer.SetScore(scored);

		currentLevel++;

		UpdateUI();

		StartALevel();
	}

	private void UpdateUI()
	{
		uiManager.SetScore(score);
		uiManager.SetLevel(currentLevel);
	}

	void Update()
	{
		stateTime += Time.deltaTime;

		switch (gameState)
		{
		case eGameState.E_STARTUP:
			break;

		case eGameState.E_MAIN_MENU:
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				OnStart();
			}
			break;

		case eGameState.E_INTRO:
			if (stateTime >= gameData.introDuration)
			{
				SetState(eGameState.E_PLAYING);
			}
			break;

		case eGameState.E_PLAYING:
			break;

		case eGameState.E_GAME_OVER:
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
			{
				SetState(eGameState.E_MAIN_MENU);
			}
			break;

		default:
			break;
		}
	}
}
