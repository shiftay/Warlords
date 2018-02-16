using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GamePlay play;
	static public GameManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RemoveFromPool(GameObject removal) {
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

	public bool V2Equal(Vector2 a, Vector2 b){
		return Vector2.SqrMagnitude(a - b) < 0.0001;
	}
}
