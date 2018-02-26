using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GamePlay play;
	static public GameManager instance;

	public int strikes = 0;
	public List<GameModes> gameModes_beaten;
	public List<GameModes> gamesModes_strike;




	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemoveFromPool(GameObject removal, bool won) {
		if(won) {
			gameModes_beaten.Add(play.activeGame[play.activeModes.IndexOf(removal)]);

		} else {

			gamesModes_strike.Add(play.activeGame[play.activeModes.IndexOf(removal)]);
			strikes--;

			if(GameOver()) {
				play.GameOver();
			}
		}


		play.activeGame.RemoveAt(play.activeModes.IndexOf(removal));
		play.activeModes.Remove(removal);

		int indexOfPuzzle = -1;
		for(int i = 0; i < play.playSpots.Length; i++) {
			if(V2Equal(removal.transform.position,play.playSpots[i].transform.position)) {
				indexOfPuzzle = i;
			}
		}

		play.usedSpots.Remove(indexOfPuzzle);
		play.poolOfSpots.Add(indexOfPuzzle);








		Destroy(removal);
	}


	bool GameOver() {
		return strikes == 0;
	}






	public bool V2Equal(Vector2 a, Vector2 b){
		return Vector2.SqrMagnitude(a - b) < 0.0001;
	}
}
