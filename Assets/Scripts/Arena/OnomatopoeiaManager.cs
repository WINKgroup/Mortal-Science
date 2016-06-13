using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnomatopoeiaManager : MonoBehaviour
{
	public static OnomatopoeiaManager Instance {get; private set;}
	
	public GameObject onomatopoeiaPrefab;

	private List<GameObject> onomatopoeias = new List<GameObject>();

	
	void Awake()
	{
		if(Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);
		
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		for(int i=0; i<5; i++)
		{
			GameObject newOnomatopoeia = Instantiate(this.onomatopoeiaPrefab, new Vector3(-1000,-1000,-1000), Quaternion.identity) as GameObject;
			newOnomatopoeia.transform.SetParent(this.transform);
			newOnomatopoeia.SetActive(false);
			this.onomatopoeias.Add(newOnomatopoeia);
		}
	}


	/// <summary>
	/// Gets a disabled onomatopoeia from a pool
	/// </summary>
	/// <returns>The onomatopoeia.</returns>
	public GameObject GetOnomatopoeia()
	{
		GameObject onom = this.onomatopoeias[0];
		
		if(onom != null)
		{
			this.onomatopoeias.RemoveAt(0);
		}
		else
		{
			onom = Instantiate(this.onomatopoeiaPrefab, new Vector3(-1000,-1000,-1000), Quaternion.identity) as GameObject;
		}
		
		return onom;
	}

	public void RecycleOnomatopoeia(GameObject hOnomatopoeia)
	{
		this.onomatopoeias.Add(hOnomatopoeia);
		hOnomatopoeia.SetActive(false);
	}
}
