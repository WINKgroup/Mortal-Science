using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseUI;
	public Button firstButton;
	bool inPause = false;
	int pauseOwner = 1;
	EventSystem eventSystem;

	void Update()
	{
		if(Game.Instance.arena.status != ArenaStatus.Fight)
			return;

		if(!inPause)
		{
			if(Input.GetButtonDown("Pause_player1"))
			{
				inPause = true;
				pauseOwner = 1;
				ShowPauseMenu(inPause);
			}
			else if(Input.GetButtonDown("Pause_player1"))
			{
				inPause = true;
				pauseOwner = 2;
				ShowPauseMenu(inPause);
			}
		}
		else
		{
			if(Input.GetButtonDown("Pause_player"+pauseOwner))
			{
				inPause = false;
				ShowPauseMenu(inPause);
			}
		}
	}

	public void Restart()
	{
		inPause = false;
		ShowPauseMenu(inPause);
		Game.Instance.StartBattle();
	}

	public void Resume()
	{
		inPause = false;
		ShowPauseMenu(inPause);
	}

	public void GoToMainMenu()
	{
		inPause = false;
		ShowPauseMenu(inPause);
		Game.Instance.GoToMainMenu();
	}

	public void ShowPauseMenu(bool show)
	{
		Time.timeScale = show ? 0 : 1;
		pauseUI.SetActive(show);

		/*if(show)
		{
			this.eventSystem = EventSystem.current;
			this.eventSystem.SetSelectedGameObject(this.firstButton);
		}*/
	}
}

