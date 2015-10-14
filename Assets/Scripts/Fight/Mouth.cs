using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mouth : MonoBehaviour
{
	private PlayerMovement player;

	private Vector2 pivotForLeftBalloon = new Vector2(0.4f, -0.4f);
	private Vector2 pivotForRightBalloon = new Vector2(0.4f, -0.4f);

	void Awake()
	{
		this.player = GetComponentInParent<PlayerMovement>();
	}

	public void SpeakRandomSentence(PlayerPosition hPos)
	{
		Balloon balloon = BalloonManager.Instance.GetBalloon().GetComponent<Balloon>();

		Vector2 spawnPosition = Camera.main.WorldToScreenPoint(this.transform.position);

		int rand;
		string text;

		if(this.player.playerID == 1)
		{
			rand = Random.Range(0, Game.Instance.player1.character.sentences.Count);
			text = Game.Instance.player1.character.sentences[rand];
		}
		else
		{
			rand = Random.Range(0, Game.Instance.player2.character.sentences.Count);
			text = Game.Instance.player2.character.sentences[rand];
		}

		balloon.Initialize(spawnPosition, text, 2);

		if(hPos == PlayerPosition.Left)
		{
			balloon.GetComponent<RectTransform>().pivot = this.pivotForLeftBalloon;
		}
		else
		{
			balloon.GetComponent<RectTransform>().pivot = this.pivotForRightBalloon;
		}
	}
}
