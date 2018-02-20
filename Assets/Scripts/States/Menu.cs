using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public Text play;
	public Text stats;
	public Text settings; 
	float fadeTimer = 0.35f;
	Color fullAlpha;
	public bool fadeIn = false;
	public bool startFade = false;
	float timer = 0f;

	public void Play() {
		StateManager.instance.ChangeState(GameStates.Main);
	}

	public void Settings() {
		StateManager.instance.ChangeState(GameStates.Settings);
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {

		timer += Time.deltaTime;

		if(timer > 1.25f && startFade) {
			fadeIn = true;
			startFade = false;
		}

		if(fadeIn) {
			FadeInAlpha(play);
			if(play.color.a >= 1) {
				fadeIn = false;
			}
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()	{
		if(StateManager.instance != null && StateManager.instance.currentState == GameStates.Menu) {
			startFade = true;


		}



	}


	void FadeInAlpha(Text num) {
		fullAlpha = num.color;
		fullAlpha.a += Time.deltaTime * fadeTimer;
		num.color = fullAlpha;
	}


	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		startFade = false;
		fadeIn = false;


		//todo: alpha on text to 0.
	}
}
