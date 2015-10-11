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

	public List<PlayerMovement> InstantiateCharacters()
	{
		this.spawn1 = GameObject.Find("Spawn1").transform;
		this.spawn2 = GameObject.Find("Spawn2").transform;
		
		GameObject char1 = Instantiate(Game.Instance.player1.character.prefab, this.spawn1.position, Quaternion.identity) as GameObject;
		GameObject char2 = Instantiate(Game.Instance.player2.character.prefab, this.spawn2.position, Quaternion.identity) as GameObject;
		
		char1.name = "Player1";
		char2.name = "Player2";

		List<PlayerMovement> playersMovement = new List<PlayerMovement>();
		playersMovement.Add(char1.GetComponent<PlayerMovement>());
		playersMovement.Add(char2.GetComponent<PlayerMovement>());

		// Set ID for character input and AI
		playersMovement[0].playerID = Game.Instance.player1.numberController;
		playersMovement[1].playerID = Game.Instance.player2.numberController;
		
		// Set Health
		playersMovement[0].maxHealth = Game.Instance.player1.health;
		playersMovement[1].maxHealth = Game.Instance.player2.health;
		playersMovement[0].health = playersMovement[0].maxHealth;
		playersMovement[1].health = playersMovement[1].maxHealth;
		
		// Let control if he needs AIEnemy or is controlled by human
		playersMovement[0].AmICpu();
		playersMovement[1].AmICpu();
		
		// Set the correct charachter's healthbar
		this.healthBar1.player = playersMovement[0];
		this.healthBar2.player = playersMovement[1];
		
		// Set the correct charachter's turbobar
		this.turboBar1.player = playersMovement[0];
		this.turboBar2.player = playersMovement[1];

		// Set the correct UI image
		this.imagePlayer1.sprite = Game.Instance.player1.character.face;
		this.imagePlayer2.sprite = Game.Instance.player2.character.face;

		// Set the correct UI player name
		this.namePlayer1.text = Game.Instance.player1.character.name;
		this.namePlayer2.text = Game.Instance.player2.character.name;

		// Set the correct UI turbo name
		this.turbonamePlayer1.text = Game.Instance.player1.character.turboName;
		this.turbonamePlayer2.text = Game.Instance.player2.character.turboName;

		return playersMovement;
	}
}
