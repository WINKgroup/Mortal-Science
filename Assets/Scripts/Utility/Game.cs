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

	public Player player1;
	public Player player2;

	void Awake()
	{
		if(Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);

		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		this.player1 = new Player(100, Scientist.Einstein, 0);
		this.player2 = new Player(100, Scientist.Galilei, 1);
	}

	public void BackOnMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void OnArcadeClick()
	{
		this.gameType = GameType.Arcade;

		this.player1.numberController = 1;
		this.player2.numberController = 0;

		Application.LoadLevel("PlayerSelect");
	}

	public void OnMultiplayerClick()
	{
		this.gameType = GameType.Multiplayer;

		this.player1.numberController = 1;
		this.player2.numberController = 2;

		Application.LoadLevel("PlayerSelect");
	}

	public void StartBattle()
	{
		Application.LoadLevel("Battle");
	}
	
}
