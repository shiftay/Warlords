using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public Text time_Txt;
	public Text puzzle_Txt;
	public Text difficulty_Txt;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		time_Txt.text = GameManager.instance.play.currentTimer.ToString();
		puzzle_Txt.text = (GameManager.instance.gameModes_beaten.Count + GameManager.instance.gamesModes_strike.Count).ToString();
	}
}
