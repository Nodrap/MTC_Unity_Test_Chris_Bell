using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
	public float levelSeparationDistance = 1;
	[Tooltip("Movement speed by level")]
	public AnimationCurve movementSpeed;

	public float scorePerfect = 3;
	public float scoreGood = 2;
	public float scoreLimit = 1;
	public float introDuration = 1;
	public float cameraLerpRate = 0.03f;
	public float cameraOffset = 0;

	[TextArea(4, 10)]
	public string scoreFormat;
	[TextArea(4, 10)]
	public string levelFormat;
	[TextArea(4, 10)]
	public string finalScoreFormat;
}