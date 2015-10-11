using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ArenaType
{
	Laboratory
}

public enum ArenaStatus
{
	Idle,
	Ready,
	Fight,
	TimeUp,
	Stop
}

public class Arena : MonoBehaviour
{
	public ArenaType type;
	public ArenaStatus status;

	public int startingTime;
	public int currentTime;
	public Text timer;

	public List<GameObject> arenaPrefabs;
	public List<GameObject> characterPrefabs;

	public HealthBarController healthBar1;
	public HealthBarController healthBar2;

	public TurboBarController turboBar1;
	public TurboBarController turboBar2;

	private Transform spawn1;
	private Transform spawn2;

	private PlayerMovement player1;
	private PlayerMovement player2;

	#region Init

	void Start()
	{
		this.SetUpArena(Game.Instance.nextArenaType);
		Camera.main.GetComponent<CameraMovement>().TakeTarget();

		this.status = ArenaStatus.Idle;

		this.startingTime = 60;
		this.timer.text = this.startingTime.ToString();
		StartCoroutine(this.Countdown(this.startingTime));
	}

	public void SetUpArena(ArenaType hType)
	{
		this.type = hType;

		switch(this.type)
		{
		case ArenaType.Laboratory:
			GameObject newArena = Instantiate(this.arenaPrefabs[0], Vector3.zero, Quaternion.identity) as GameObject;
			newArena.transform.SetParent(this.transform);
			break;
		}

		this.spawn1 = GameObject.Find("Spawn1").transform;
		this.spawn2 = GameObject.Find("Spawn2").transform;

		GameObject char1 = Instantiate(this.characterPrefabs[Game.Instance.character1.characterID], this.spawn1.position, Quaternion.identity) as GameObject;
		GameObject char2 = Instantiate(this.characterPrefabs[Game.Instance.character2.characterID], this.spawn2.position, Quaternion.identity) as GameObject;

		char1.name = "Player1";
		char2.name = "Player2";

		this.player1 = char1.GetComponent<PlayerMovement>();
		this.player2 = char2.GetComponent<PlayerMovement>();

		// Set ID for character input and AI
		this.player1.playerID = Game.Instance.character1.numberController;
		this.player2.playerID = Game.Instance.character2.numberController;

		// Set Health
		this.player1.maxHealth = Game.Instance.character1.health;
		this.player2.maxHealth = Game.Instance.character2.health;
		this.player1.health = this.player1.maxHealth;
		this.player2.health = this.player2.maxHealth;

		// Let control if he needs AIEnemy or is controlled by human
		this.player1.AmICpu();
		this.player2.AmICpu();

		// Set the correct charachter's healthbar
		this.healthBar1.character = this.player1;
		this.healthBar2.character = this.player2;

		// Set the correct charachter's turbobar
		this.turboBar1.character = this.player1;
		this.turboBar2.character = this.player2;
	}

	#endregion

	void Update()
	{
		if(this.player1.health <= 0 || this.player2.health <= 0)
		{
			this.status = ArenaStatus.Stop;
		}
	}

	private IEnumerator Countdown(int time)
	{
		this.currentTime = time;

		this.status = ArenaStatus.Fight;

		while(this.currentTime > 0 && this.status == ArenaStatus.Fight)
		{
			this.currentTime--;
			this.timer.text = this.currentTime.ToString();

			if(this.currentTime <= 0)
			{
				this.status = ArenaStatus.TimeUp;
				this.timer.text = "0";
			}

			yield return new WaitForSeconds(1);
		}
	}
}

