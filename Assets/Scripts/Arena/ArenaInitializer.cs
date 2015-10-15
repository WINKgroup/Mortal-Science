using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ArenaInitializer : MonoBehaviour
{
	public List<GameObject> arenaPrefabs;

	public HealthBarController healthBar1;
	public HealthBarController healthBar2;
	
	public TurboBarController turboBar1;
	public TurboBarController turboBar2;

	public Text namePlayer1;
	public Text namePlayer2;
	
	public Text turbonamePlayer1;
	public Text turbonamePlayer2;

	public Image imagePlayer1;
	public Image imagePlayer2;

	private Transform spawn1;
	private Transform spawn2;


	public void InstantiateArenaPrefab(ArenaType hArenaType)
	{
		switch(hArenaType)
		{
		case ArenaType.Laboratory:
			GameObject newArena = Instantiate(this.arenaPrefabs[0], Vector3.zero, Quaternion.identity) as GameObject;
			newArena.transform.SetParent(this.transform);
			break;
		}
	}

	public void InstantiateCharacters()
	{
		this.spawn1 = GameObject.Find("Spawn1").transform;
		this.spawn2 = GameObject.Find("Spawn2").transform;
		
		GameObject char1 = Instantiate(Game.Instance.player1.character.prefab, this.spawn1.position, Quaternion.identity) as GameObject;
		GameObject char2 = Instantiate(Game.Instance.player2.character.prefab, this.spawn2.position, Quaternion.identity) as GameObject;

		// Set the correct instance of Player GameObject for Game.player
		Game.Instance.player1.gameInstance = char1;
		Game.Instance.player2.gameInstance = char2;

		char2.transform.localScale = new Vector3(-char2.transform.localScale.x, char2.transform.localScale.y, char2.transform.localScale.z);
		
		char1.name = "Player1";
		char2.name = "Player2";

		PlayerMovement playersMovement1 = char1.GetComponent<PlayerMovement>();
		PlayerMovement playersMovement2 = char2.GetComponent<PlayerMovement>();

		// Set ID for character input and AI
		playersMovement1.playerID = Game.Instance.player1.numberController;
		playersMovement2.playerID = Game.Instance.player2.numberController;
		
		// Set Health
		playersMovement1.maxHealth = Game.Instance.player1.health;
		playersMovement2.maxHealth = Game.Instance.player2.health;
		playersMovement1.health = playersMovement1.maxHealth;
		playersMovement2.health = playersMovement2.maxHealth;
		
		// Let control if he needs AIEnemy or is controlled by human
		playersMovement1.AmICpu();
		playersMovement2.AmICpu();
		
		// Set the correct charachter's healthbar
		this.healthBar1.player = playersMovement1;
		this.healthBar2.player = playersMovement2;
		
		// Set the correct charachter's turbobar
		this.turboBar1.player = playersMovement1;
		this.turboBar2.player = playersMovement2;

		// Set the correct UI image
		this.imagePlayer1.sprite = Game.Instance.player1.character.face;
		this.imagePlayer2.sprite = Game.Instance.player2.character.face;

		// Set the correct UI player name
		this.namePlayer1.text = Game.Instance.player1.character.name;
		this.namePlayer2.text = Game.Instance.player2.character.name;

		// Set the correct UI turbo name
		this.turbonamePlayer1.text = Game.Instance.player1.character.turboName;
		this.turbonamePlayer2.text = Game.Instance.player2.character.turboName;
	}
}
