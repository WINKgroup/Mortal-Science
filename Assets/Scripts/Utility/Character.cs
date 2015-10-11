using UnityEngine;
using System.Collections;

[System.Serializable]
public class Character
{
	public int health;

	public string name;

	public int characterID;

	public int numberController;

	public Character ()
	{
		this.health = 100;
		this.name = "Player";
		this.characterID = 0;
		this.numberController = 0;
	}

	public Character (int iHealth, string sName, int iID, int iNumber)
	{
		this.health = iHealth;
		this.name = sName;
		this.characterID = iID;
		this.numberController = iNumber;
	}

	public void SetNumber(int iN)
	{
		this.numberController = iN;
	}

	public void SetID(int iID)
	{
		this.characterID = iID;
	}

	public void SetHealth(int iHealth)
	{
		this.health = iHealth;
	}
}

