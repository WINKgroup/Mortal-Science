using UnityEngine;
using System.Collections;

public enum GameType
{
	Arcade, Multiplayer
}

public class Game : MonoBehaviour
{
	public static Game Instance {get; private set;}
	
	public Arena arena;
	public ArenaType nextArenaType;
	public GameType gameType;

	public Character character1 = new Character(100, "Einstein", 0, 1);
	public Character character2 = new Character(100, "Galilei", 1, 0);

	void Awake()
	{
		if(Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	public void BackOnMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void OnArcadeClick()
	{
		this.gameType = GameType.Arcade;

		this.character1.numberController = 1;
		this.character2.numberController = 0;

		Application.LoadLevel("PlayerSelect");
	}

	public void OnMultiplayerClick()
	{
		this.gameType = GameType.Multiplayer;

		this.character1.numberController = 1;
		this.character2.numberController = 2;

		Application.LoadLevel("PlayerSelect");
	}

	public void StartBattle()
	{
		Application.LoadLevel("Battle");
	}
	
}
