using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour {

	public string[] GameWords;

	public Text[] letters;
	int currentWord;
	string answerKey;
	public List<int> eachPosindex = new List<int>();
	bool notmixed = false;
	bool isWon = false;
	public float timer = 0f;
	public bool gameStarted = false;

	// Use this for initialization
	void Start () {
		
		currentWord = Random.Range(0,GameWords.Length);
		answerKey = GameWords[currentWord];

		List<int> used = new List<int>();


			for(int i = 0; i < letters.Length; i++) {
				int currentNum = 0;
				do {
					currentNum = Random.Range(0,answerKey.Length);
				} while(used.Contains(currentNum));
				
				used.Add(currentNum);

				letters[i].text = answerKey[currentNum].ToString();
				eachPosindex.Add(currentNum);
			}



	}


	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if(!GetComponent<Animation>().isPlaying && !gameStarted) {
			gameStarted = true;
		}

		if(gameStarted) {
			if(GameWon()) {
				GameManager.instance.RemoveFromPool(this.gameObject, true);
			}

			timer += Time.deltaTime;

			if(timer > 15f) {
				GameManager.instance.RemoveFromPool(this.gameObject, false);
			}
		}




	}



	bool GameWon() {
		bool retVal = true;

		for(int i = 0; i < answerKey.Length; i++) {
			if(eachPosindex[i] != i) {
				retVal = false;
			}
		}

		return retVal;
	}




	public void Next(int position) {
		if(!isWon) {
			eachPosindex[position]++;
			if(eachPosindex[position] >= answerKey.Length) {
				eachPosindex[position] = 0;
			}


			letters[position].text = answerKey[eachPosindex[position]].ToString();
		}
	}

	public void Back(int position) {
		if(!isWon) {
			eachPosindex[position]--;
			if(eachPosindex[position] < 0) {
				eachPosindex[position] = answerKey.Length - 1;
			}


			letters[position].text = answerKey[eachPosindex[position]].ToString();
		}
	}
}
