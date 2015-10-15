using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UISpriteTurboAnimation : MonoBehaviour
{
	public int playerID;

	private bool turbo;
	private Image image;
	private List<Sprite> frames = new List<Sprite>();

	void Awake()
	{
		this.image = GetComponent<Image>();
		this.turbo = false;
	}

	void OnEnable()
	{
		StartCoroutine(this.AnimateTurbo());
	}

	void OnDisable()
	{
		this.turbo = false;

		if(Game.Instance.arena.status == ArenaStatus.Turbo)
		{
			Game.Instance.arena.status = ArenaStatus.Fight;
		}
	}

	public IEnumerator AnimateTurbo()
	{
		this.turbo = true;
		Game.Instance.arena.status = ArenaStatus.Turbo;
	
		for(int i = 0; i < this.frames.Count && this.turbo; i++)
		{
			this.image.sprite = this.frames[i];

			if(i == this.frames.Count - 1)
				i = -1;

			yield return new WaitForSeconds(0.1f);
		}
	}

	public void LoadFrames()
	{
		switch(this.playerID)
		{
		case 1:
			this.frames = Game.Instance.player1.character.uiTurboFrame;
			break;
		default:
			this.frames = Game.Instance.player2.character.uiTurboFrame;
			break;
		}
	}
}
