using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CanvasMenuController : MonoBehaviour
{
	//public Button startingButton;

	private EventSystem eventSystem;


	void Start ()
	{
		/*this.eventSystem = EventSystem.current;
		this.eventSystem.firstSelectedGameObject = this.startingButton.gameObject;//SetSelectedGameObject(this.startingButton, new BaseEventData(this.eventSystem));*/
	}

	public void StartArcade()
	{
		Game.Instance.OnArcadeClick();
	}

	public void StartMultiplayer()
	{
		Game.Instance.OnMultiplayerClick();
	}

	public void CloseGame()
	{
		Game.Instance.ExitGame();
	}
}
