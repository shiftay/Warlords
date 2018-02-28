using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModes { BalanceBeam, Breakout, SimonSays, QuickMath, AlphabetScramble, ScrambledWords, Rullo, Maze };

public class GamePlay : MonoBehaviour {

	public GameObject[] playSpots;
	public GameObject[] puzPrefab;
	public float gameTimer = 0f;
	public float currentTimer = 0f;
	public bool gameOver = false;
	public List<GameModes> activeGame = new List<GameModes>();
	public List<GameObject> activeModes = new List<GameObject>();
	public List<GameModes> listOfModes = new List<GameModes>();

	public List<int> poolOfSpots = new List<int>();
	public List<int> usedSpots = new List<int>();



	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		if(StateManager.instance != null && StateManager.instance.currentState == GameStates.Main) {
			GameManager.instance.strikes = 3;


			gameOver = false;
			gameTimer = 0;

			activeGame.Clear();
			listOfModes.Clear();

			//TODO: for loop?
			poolOfSpots.Add(0);
			poolOfSpots.Add(1);
			poolOfSpots.Add(2);

			GameManager.instance.strikes = 3;
			GameManager.instance.gameModes_beaten.Clear();
			GameManager.instance.gamesModes_strike.Clear();

			int first = Random.Range(0,puzPrefab.Length);
			activeGame.Add((GameModes)first);
			listOfModes.Add((GameModes)first);
			int spot = Random.Range(0,playSpots.Length);
			activeModes.Add(Instantiate(puzPrefab[first], playSpots[spot].transform.position, Quaternion.identity));
			activeModes[activeModes.Count - 1].transform.SetParent(this.transform);
			poolOfSpots.Remove(spot);
			usedSpots.Add(spot);




		}
	}

	// Update is called once per frame
	void Update () {
		if(!gameOver) {
			gameTimer += Time.deltaTime;
			currentTimer += Time.deltaTime;

			if(gameTimer > 10) {
				gameTimer = 0;
				if(usedSpots.Count != 3) { // Magic number?
					int newSpot;
					do {
						newSpot = Random.Range(0, playSpots.Length);
					} while(usedSpots.Contains(newSpot));

					int newGame;
					do{
						newGame = Random.Range(0,puzPrefab.Length);
					} while(activeGame.Contains((GameModes)newGame));

					activeGame.Add((GameModes)newGame);
					listOfModes.Add((GameModes)newGame);
					activeModes.Add(Instantiate(puzPrefab[newGame], playSpots[newSpot].transform.position, Quaternion.identity));
					activeModes[activeModes.Count - 1].transform.SetParent(this.transform);
					poolOfSpots.Remove(newSpot);
					usedSpots.Add(newSpot);
				}

			}
		}
	}



	public void GameOver() {
		gameOver = true;

		Debug.Log("GAME OVER");

		for(int i = 0; i < activeModes.Count; i++) {
			Destroy(activeModes[i]);
		}

		activeGame.Clear();
		listOfModes.Clear();
		poolOfSpots.Clear();
		usedSpots.Clear();

		StateManager.instance.ChangeState(GameStates.GameOver);
		

	}

}


