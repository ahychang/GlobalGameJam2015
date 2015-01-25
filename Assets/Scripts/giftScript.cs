using UnityEngine;
using System.Collections;

public class giftScript : MonoBehaviour {

	// y position should be 9
	public GameManager gm;
	AudioSource sound;

	void Start()
	{
		sound = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player")
		{
			DontDestroy.me.gameObject.GetComponentInChildren<ScoreUI>().AddScore2();
			sound.Play ();
			gm.doSpawn = false;
			Destroy (gameObject, 0.83f);
		}
	}
}
