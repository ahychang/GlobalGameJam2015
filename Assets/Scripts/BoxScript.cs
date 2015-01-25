using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour {
	public bool canHurt;
	AudioSource collideSound;

	void Start()
	{
		canHurt = true;
		collideSound = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Floor" ||
		    (coll.gameObject.tag == "Box" && coll.gameObject.GetComponent<BoxScript>().canHurt == false))
		{
			if (canHurt) collideSound.Play ();
			canHurt = false;
		}

	}
}
