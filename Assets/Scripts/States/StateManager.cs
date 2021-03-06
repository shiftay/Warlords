﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates { Loading, Menu, Main, GameOver, Settings, BackGroundRaw, BackGroundIMG }


public class StateManager : MonoBehaviour {

	List<GameObject> states = new List<GameObject>();
	public GameStates currentState;
	public GameStates previousState;
	public static StateManager instance; 

	public bool actualStartup = false;

	// Use this for initialization
	void Start () {
		instance = this;
		for(int i = 0; i < transform.childCount; i++) {
			states.Add(transform.GetChild(i).gameObject);
		}

		if(actualStartup) {
			foreach (GameObject states in states) {
				states.SetActive(false);
			}
		}

		states[(int)GameStates.BackGroundRaw].SetActive(true);

		ChangeState(GameStates.Loading);
	}

	public void ChangeState(GameStates test) {
		previousState = currentState;
		states[(int)currentState].SetActive(false);
		currentState = test;
		states[(int)currentState].SetActive(true);
	}

}
