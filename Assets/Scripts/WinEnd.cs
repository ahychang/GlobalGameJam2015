using UnityEngine;
using System.Collections;

public class WinEnd : MonoBehaviour 
{
	// SOUND
	AudioSource doorCreak;
	float endTime;

	void Start() 
	{
		endTime = -999f;
		// SOUND
		doorCreak = GetComponent<AudioSource> ();
		if (PlayerPrefs.GetString ("Sound") == "True") 
			doorCreak.Play ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if (Application.loadedLevelName != "Tutorial")
				GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<ScoreUI>().AddScore();
			if (PlayerPrefs.GetString ("Sound") == "True") 
				doorCreak.Play ();
			endTime = Time.time;
		}	
	}

	void Update ()
	{
		if (endTime > 0 && Time.time > endTime + 0.3f)
			Application.LoadLevel("Intermediate");
	}
}
