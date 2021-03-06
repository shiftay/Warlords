﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

	public float timer = 0f;
	public float LOADTIME = 5f;
	
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		timer = 0;
	}


	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > LOADTIME) {
			StateManager.instance.ChangeState(GameStates.Menu);
		}
	}
}
