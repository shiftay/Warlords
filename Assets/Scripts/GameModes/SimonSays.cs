using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SimonSaysState { INPUT, RESOLUTION, SHOWING }
public enum SimonSaysColors { RED, BLUE, WHITE, GREEN }

public class SimonSays : MonoBehaviour {

	public int strikes = 0;
	const int MINAMT = 3;
	public int MAXAMT;
	public int currentLevel;
	public int counter = 0;
	public SimonSaysState currentState;
	public List<SimonSaysColors> playerChoices = new List<SimonSaysColors>();
	public List<SimonSaysColors> puzzle = new List<SimonSaysColors>();
	public Button[] currentButtons;
	private float fadeTimer = 0.35f;
	public float timer;
	public float TIMED;
	private ColorBlock fullAlpha;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		currentLevel = MINAMT;
		MAXAMT = Random.Range(MINAMT, 6); // TODO: Remove magic number?
		CreatePuzzle();
		deactivateBtns();
		currentState = SimonSaysState.SHOWING;
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case SimonSaysState.INPUT:
				if(playerChoices.Count >= puzzle.Count) {
					currentState = SimonSaysState.RESOLUTION;
					deactivateBtns();
				}

				timer += Time.deltaTime;


				if(timer >= 5 && playerChoices.Count == 0) {
					//switch to showing again.
					// TODO: show it again, and to give them a shot.
				}


				break;
			case SimonSaysState.RESOLUTION:
				if(Resolution()) {
					if(MAXAMT == currentLevel) {
						GameManager.instance.RemoveFromPool(this.gameObject);
					}

					currentLevel++;
					CreatePuzzle();
					currentState = SimonSaysState.SHOWING;
					timer = 0;


				} else {
					if(strikes != 3) {
						strikes++;
						currentState = SimonSaysState.SHOWING;
						timer = 0;
					} else {
						Debug.Log("GAMEOVER DUDE!");
					}
				}

				playerChoices.Clear();

				break;
			case SimonSaysState.SHOWING:
				Showing();
				break;
		}
	}




	void Showing() {
		// ColorBlock cb = currentButtons[counter].colors;
		// cb.disabledColor = Color32.Lerp(dullColor, vibrantColor, Mathf.PingPong(speed, Time.deltaTime));
		// currentButtons[counter].colors = cb;
		timer += Time.deltaTime;
		if(timer <= TIMED) {
			FadeInAlpha((int)puzzle[counter]);
		}else if(TIMED < timer && timer < (TIMED * 2)) {
			FadeOutAlpha((int)puzzle[counter]);
		}else if(timer >= (TIMED * 2) + 1) {
			counter++;
			timer = 0;
			if(counter >= puzzle.Count) {
				currentState = SimonSaysState.INPUT;
				counter = 0;
				foreach (Button curBtn in currentButtons) {
					curBtn.interactable = true;
				}
			}
		}
	}



	bool Resolution() {
		bool retVal = true; 
		
		for(int i = 0; i < puzzle.Count; i++) {			
			if(puzzle[i] != playerChoices[i]){
				retVal = false;
			}
		}

		return retVal;
	}

	void FadeOutAlpha(int num) {
		fullAlpha = currentButtons[num].colors;
		Color disabledcolor = fullAlpha.disabledColor;
		disabledcolor.a -= Time.deltaTime * fadeTimer;
		fullAlpha.disabledColor = disabledcolor;
		currentButtons[num].colors = fullAlpha;
	}

	void FadeInAlpha(int num) {
		fullAlpha = currentButtons[num].colors;
		Color disabledcolor = fullAlpha.disabledColor;
		disabledcolor.a += Time.deltaTime * fadeTimer;
		fullAlpha.disabledColor = disabledcolor;
		currentButtons[num].colors = fullAlpha;
	}


	void CreatePuzzle() {
		if(puzzle.Count > 0) {
			puzzle.Clear();
		}

		for(int i = 0; i < currentLevel; i++) {
			puzzle.Add((SimonSaysColors)Random.Range(0,4));
		}

	}




// -------- BUTTONS --------
	void deactivateBtns() {
		foreach (Button button in currentButtons) {
			button.interactable = false;
		}
	}



	public void AddToGreen() {
		playerChoices.Add(SimonSaysColors.GREEN);
	}

	public void AddToBlue() {
		playerChoices.Add(SimonSaysColors.BLUE);
	}

	public void AddToRed() {
		playerChoices.Add(SimonSaysColors.RED);
	}

	public void AddToWhite() {
		playerChoices.Add(SimonSaysColors.WHITE);
	}
// -------- BUTTONS --------
}
