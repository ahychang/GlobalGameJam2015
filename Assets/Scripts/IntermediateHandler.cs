﻿using UnityEngine;
using System.Collections;

public class IntermediateHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("return")) {
			Application.LoadLevel("Main");
		}
	}
}