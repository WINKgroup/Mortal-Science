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

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			Cursor.visible = false;
		}
		else
		{
			Destroy(this.gameObject);
		}
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
		Application.LoadLevel("Battle");
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
	
}
