using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float minTime = 5f; 
	public float maxTime = 10f;
	public float x = 5;
	public float minX = -5.5f;
	public float maxX = 5.5f;
	public float topY = 15f;
	public float z = 0.0f;
	public int count = 200;
	public GameObject prefab;
	public GameObject player;
	
	public bool doSpawn = true;

	float intensity;
	
	void Update() {
		maxX = player.transform.position.x+x;
		minX = player.transform.position.x-2;
	}

	void Start() {
		StartCoroutine(Spawner());
		intensity = 1f;
		if (DontDestroy.me != null)
		{
			intensity = (float) DontDestroy.me.gameObject.GetComponentInChildren<ScoreUI>().GetScore();
			intensity /= 10;
		}
	}
	
	IEnumerator Spawner() {
		while (doSpawn && count > 0) {
				if (intensity < 1) intensity = 1f;
				Vector3 v = new Vector3 (Random.Range (minX, maxX), topY, z);
				Instantiate (prefab, v, Quaternion.Euler( 0 , 0 , Random.Range(0, 360)));
				count--;
				yield return new WaitForSeconds (Random.Range (minTime/intensity, maxTime/intensity));
		}
	}
}
