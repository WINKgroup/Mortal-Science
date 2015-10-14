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
	Turbo,
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

	public GameObject readyFightUI;

	#region Init

	void Awake()
	{
		this.initializer = this.GetComponent<ArenaInitializer>();
		this.readyFightUI.SetActive(false);
		this.status = ArenaStatus.Ready;
	}

	void Start()
	{
		Game.Instance.arena = this;

		this.SetUpArena(Game.Instance.nextArenaType);

		this.status = ArenaStatus.Idle;

		this.startingTime = 60;
		this.timer.text = this.startingTime.ToString();
	}

	public void SetUpArena(ArenaType hArenaType)
	{
		this.initializer.InstantiateArenaPrefab(hArenaType);
		this.players = this.initializer.InstantiateCharacters();
		Camera.main.GetComponent<CameraMovement>().TakePlayers();
		this.readyFightUI.SetActive(true);
	}

	#endregion

	void Update()
	{
		if(this.players[0].health <= 0 || this.players[1].health <= 0)
		{
			this.status = ArenaStatus.Stop;
		}
	}

	public void StartMatch()
	{
		this.status = ArenaStatus.Fight;
		StartCoroutine(this.Countdown(this.startingTime));
	}

	private IEnumerator Countdown(int time)
	{
		this.currentTime = time;

		while(this.status == ArenaStatus.Fight || this.status == ArenaStatus.Turbo)
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

