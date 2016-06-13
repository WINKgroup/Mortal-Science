using UnityEngine;
using System.Collections;

public enum GameType
{
	Menu, Arcade, Multiplayer
}

public class Game : MonoBehaviour
{
	public static Game Instance {get; private set;}
	
	public Arena arena;
	public ArenaType nextArenaType;
	public GameType gameType;

	public Player player1;
	public Player player2;

	public GameObject loadingObject;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			Cursor.visible = false;
			#if UNITY_EDITOR
			Cursor.visible = true;
			#endif
		}
		else
		{
			Destroy(this.gameObject);
		}

		ShowLoadingContent(false);
	}

	public void BackOnMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void OnArcadeClick()
	{
		this.gameType = GameType.Arcade;

		this.player1 = new Player(100, Scientist.Einstein, 1);
		this.player2 = new Player(100, Scientist.Galilei, 0);

		this.StartBattle();

		//Application.LoadLevel("PlayerSelect");
	}

	public void OnMultiplayerClick()
	{
		this.gameType = GameType.Multiplayer;

		this.player1 = new Player(100, Scientist.Einstein, 1);
		this.player2 = new Player(100, Scientist.Galilei, 2);

		this.StartBattle();

		//Application.LoadLevel("PlayerSelect");
	}

	public void StartBattle()
	{
		ShowLoadingContent(true);
		Application.LoadLevel("Battle");
	}

	public void GoToMainMenu()
	{
		ShowLoadingContent(true);
		Application.LoadLevel("MainMenu");
	}

	public void ShowLoadingContent(bool show)
	{
		this.loadingObject = GameObject.Find("Loading");
		if(this.loadingObject == null)
			return;

		this.loadingObject.SetActive(show);
	}
	
}
