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
	public ArenaStatus status;

	public int startingTime;
	public int currentTime;
	public Text timer;

	private ArenaInitializer initializer;

	private List<PlayerMovement> players;

	#region Init

	void Awake()
	{
		this.initializer = this.GetComponent<ArenaInitializer>();
	}

	void Start()
	{
		this.SetUpArena(Game.Instance.nextArenaType);
		Camera.main.GetComponent<CameraMovement>().TakeTarget();

		this.status = ArenaStatus.Idle;

		this.startingTime = 60;
		this.timer.text = this.startingTime.ToString();
		StartCoroutine(this.Countdown(this.startingTime));
	}

	public void SetUpArena(ArenaType hArenaType)
	{
		this.initializer.InstantiateArenaPrefab(hArenaType);
		this.players = this.initializer.InstantiateCharacters();
	}

	#endregion

	void Update()
	{
		if(this.players[0].health <= 0 || this.players[1].health <= 0)
		{
			this.status = ArenaStatus.Stop;
		}
	}

	private IEnumerator Countdown(int time)
	{
		this.currentTime = time;

		this.status = ArenaStatus.Fight;

		while(this.status == ArenaStatus.Fight)
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

