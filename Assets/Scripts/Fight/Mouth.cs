using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mouth : MonoBehaviour
{
	private PlayerMovement player;

	void Awake()
	{
		this.player = GetComponentInParent<PlayerMovement>();
	}

	public void SpeakRandomSentence()
	{
		Balloon balloon = BalloonManager.Instance.GetBalloon().GetComponent<Balloon>();

		Vector2 spawnPosition = Camera.main.WorldToScreenPoint(this.transform.position);

		int rand;
		string text;

		if(this.player.playerID == 1)
		{
			rand = Random.Range(0, Game.Instance.player1.character.sentences.Count);
			text = Game.Instance.player1.character.sentences[rand];
			balloon.Initialize(spawnPosition, text, 2);
		}
		else
		{
			rand = Random.Range(0, Game.Instance.player2.character.sentences.Count);
			text = Game.Instance.player2.character.sentences[rand];
			balloon.Initialize(spawnPosition, text, 2);
		}
	}
}
