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
	Stop,
	End
}

public enum PlayerPosition
{
	Left,
	Right
}

public class Arena : MonoBehaviour
{
	public ArenaStatus status;

	public int startingTime;
	public int currentTime;
	public Text timer;

	private ArenaInitializer initializer;

	private PlayerMovement player1;
	private PlayerMovement player2;

	public GameObject readyFightUI;
	public Text winText;

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
		this.initializer.InstantiateCharacters();
		this.player1 = Game.Instance.player1.gameInstance.GetComponent<PlayerMovement>();
		this.player2 = Game.Instance.player2.gameInstance.GetComponent<PlayerMovement>();

		Camera.main.GetComponent<CameraMovement>().TakePlayers();
		this.readyFightUI.SetActive(true);
	}

	#endregion

	void Update()
	{
		if((this.player1.health <= 0 || this.player2.health <= 0) && this.status != ArenaStatus.End)
		{
			this.status = ArenaStatus.Stop;
		}

		if(this.status == ArenaStatus.Stop)
		{
			this.EndMatch();
			this.status = ArenaStatus.End;
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

	public void EndMatch()
	{
		if(this.player1.health > 0)
		{
			this.winText.text = Game.Instance.player1.character.name;
		}
		else
		{
			this.winText.text = Game.Instance.player2.character.name;
		}

		this.winText.text += "\n<color=#c20f0f>vince!</color>";
		this.winText.transform.parent.gameObject.SetActive(true);
	}	
}

