using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
	public GameObject arenaVisuals;

	public GameObject levelPrefab;

	public GameData gameData;

	[Header("RUNTIME")]
	public int levelCount;
	public List<GameObject> levels;

	public void ShowArena(bool show = true)
	{
		arenaVisuals.SetActive(show);
	}

	public void Reset()
	{
		foreach (var item in levels)
		{
			Destroy(item);
		}
		levels.Clear();

		levelCount = 0;
	}

	// Returns the player object for this level
	public PlayerController AddLevel()
	{
		levelCount++;

		GameObject levelObject = Instantiate(levelPrefab, GameManager.sInst.levelOrigin.transform);

		levelObject.transform.localPosition = new Vector3(0, 0, levelCount * gameData.levelSeparationDistance);

		PlayerController player = levelObject.GetComponent<PlayerController>();
		player.Init();

		levels.Add(levelObject);

		return player;

	}
}
