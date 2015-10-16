using UnityEngine;
using System.Collections;

public class WinMenu : MonoBehaviour
{
	public void ReloadLevel()
	{
		/*switch(Game.Instance.gameType)
		{
		case GameType.Arcade:
			Game.Instance.OnArcadeClick();
			break;
		case GameType.Multiplayer:
			Game.Instance.OnMultiplayerClick();
			break;
		}*/

		Game.Instance.StartBattle();
	}

	public void MainMenu()
	{
		Game.Instance.GoToMainMenu();
	}
}
