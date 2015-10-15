using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISpriteTurboAnimation : MonoBehaviour
{
	public int playerID;

	private bool turbo;
	private SpriteRenderer spriteRenderer;
	private List<Sprite> frames = new List<Sprite>();

	void Awake()
	{
		this.spriteRenderer = GetComponent<SpriteRenderer>();
		this.turbo = false;
	}

	void OnDisable()
	{
		this.turbo = false;
	}

	public IEnumerator AnimateTurbo()
	{
		this.turbo = true;
	
		for(int i = 0; i < this.frames.Count && this.turbo; i++)
		{
			this.spriteRenderer.sprite = this.frames[i];

			if(i == this.frames.Count - 1)
				i = -1;

			yield return null;
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
