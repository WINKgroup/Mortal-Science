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
	public Text drawText;

	public TerrainCollider groundSurface;

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

		if(this.status == ArenaStatus.TimeUp)
		{
			//this.DrawMatch();
			this.EndMatch();
			this.status = ArenaStatus.End;
		}
	}

	public void StartMatch()
	{
		this.status = ArenaStatus.Fight;
		Game.Instance.ShowLoadingContent(false);
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
		if(this.player1.health > this.player2.health)
		{
			this.winText.text = Game.Instance.player1.character.name;
		}
		else if(this.player1.health < this.player2.health)
		{
			this.winText.text = Game.Instance.player2.character.name;
		}
		else
		{
			this.DrawMatch();
			return;
		}

		this.winText.text += "\n<color=#c20f0f>vince!</color>";
		this.winText.transform.parent.gameObject.SetActive(true);
	}

	public void DrawMatch()
	{
		this.drawText.transform.parent.gameObject.SetActive(true);
	}

	public Vector3 GetRandomPointOnSurfaceNear(Vector3 originPosition, float range)
	{
		Vector3 randomPos = (Random.insideUnitSphere * range) + originPosition;

		Vector3 terrain = this.groundSurface.transform.localPosition;
		float depth = this.groundSurface.bounds.size.z;
		float z = 0;
		if(randomPos.z < terrain.z - depth/2)
			z = terrain.z - depth/2;
		if(randomPos.z > terrain.z + depth/2)
			z = terrain.z + depth/2;

		return new Vector3(randomPos.x, 0f, z);
	}
}

