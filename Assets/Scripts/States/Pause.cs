using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public Text time_Txt;
	public Text puzzle_Txt;
	public Text difficulty_Txt;
	public Text[] strikes;
	public float difficultyRating;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		time_Txt.text = GameManager.instance.play.currentTimer.ToString("0.00") + "s";
		puzzle_Txt.text = (GameManager.instance.gameModes_beaten.Count + GameManager.instance.gamesModes_strike.Count).ToString();
		difficultyRating = GameManager.instance.CalculateDifficulty();
		difficulty_Txt.text = difficultyRating.ToString("0.00");

		for(int i = 0; i < GameManager.instance.gamesModes_strike.Count; i++) {
			strikes[i].text = GameManager.instance.gamesModes_strike[i].ToString();
		}

	}


	public void PlayAgain() {
		StateManager.instance.ChangeState(GameStates.Main);
	}
}
