using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
	public static int currentLevel = 1;
	public static float prevLevelSeconds = 0;
	public static float prevLevelMinutes = 0;

	public static void CompleteLevel()
	{
		currentLevel++;
		Application.LoadLevel("Level " + currentLevel);
	}

	public static void RestartLevel()
	{
		Application.LoadLevel("Level " + currentLevel);
	}
}
