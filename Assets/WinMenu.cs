using UnityEngine;
using System.Collections;

public class WinMenu : MonoBehaviour
{
	public void ReloadLevel()
	{
		Game.Instance.StartBattle();
	}

	public void MainMenu()
	{
		Game.Instance.GoToMainMenu();
	}
}
