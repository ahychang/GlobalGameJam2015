using UnityEngine;
using System.Collections;

public class GGJRestart : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<ScoreUI>().ResetScore();
			Application.LoadLevel("Main");
		}
		else if(Input.GetKeyDown(KeyCode.Home))
			Application.LoadLevel("Start");
	}
}
