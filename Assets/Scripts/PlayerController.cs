using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
	public GameObject startPosition;
	public GameObject player;
	public TMP_Text finalScore;

	public GameData gameData;

	public event Action<GameManager.eScoreZone> OnPlayerStopped;

	[Header("RUNTIME")]
	public float movingDirection;

	public bool isMoving;

	internal void Init()
	{
		movingDirection = Random.Range(0, 1.0f) > 0.5f ? -1 : 1;

		player.transform.localPosition = startPosition.transform.localPosition * -movingDirection;

		isMoving = true;
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isMoving)
		{
			Vector3 pos = player.transform.localPosition;

			pos.x += movingDirection * gameData.movementSpeed.Evaluate(GameManager.sInst.currentLevel) * Time.deltaTime;

			player.transform.localPosition = pos;

			// Finish if outside starting position or user pressed key
			if ((Mathf.Abs(pos.x) > startPosition.transform.localPosition.x)
				|| (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
			{
				StopPlayer();
			}
		}
	}

	public void StopPlayer()
	{
		isMoving = false;

		// Evaluate score

		Collider[] colliders = Physics.OverlapBox(player.transform.position, player.transform.localScale * 0.5f);

		int level = 0;

		foreach (var item in colliders)
		{
			int foundLevel = 0;

			if (item.CompareTag("Fail")) foundLevel = 3;
			if (item.CompareTag("LimitZone")) foundLevel = 2;
			if (item.CompareTag("GoodZone")) foundLevel = 1;

			if (foundLevel > level) level = foundLevel;
		}

		OnPlayerStopped?.Invoke((GameManager.eScoreZone)level);
	}

	public void SetScore(float score)
	{
		finalScore.text = score.ToString();
	}
}
