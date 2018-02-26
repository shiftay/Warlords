using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReorderPuzzle : MonoBehaviour {

	public GameObject[] boxes;
	public Vector2[] startPositions;

	public Vector2[] puzzleStarts;
	public MousedownScript mousedown;
	public List<GameObject> puzDelim = new List<GameObject>();
	public GameObject[] currentPuz;
	public float timer = 0f;
	public int puzzleSize;
	public GameObject[] answerKey;
	public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	public string numbers = "123456789";

	public bool gameStart = false;
	bool mixed = true;


	// Use this for initialization
	void Start () {
		// startPositions = new Vector2[] { boxes[0].transform.position, boxes[1].transform.position, boxes[2].transform.position, boxes[3].transform.position, boxes[4].transform.position, boxes[5].transform.position };
		timer = 0;
		startPositions = new Vector2[] { boxes[0].transform.position, boxes[1].transform.position, boxes[2].transform.position, boxes[3].transform.position, boxes[4].transform.position, boxes[5].transform.position };
		puzzleSize = Random.Range(3, 7);

		currentPuz = new GameObject[puzzleSize];
		
		for(int i = puzzleSize; i < boxes.Length; i++) {
			boxes[i].SetActive(false);
		}
		

		string puzzle = alphabet.Substring(Random.Range(0, 25-puzzleSize), puzzleSize);

		for(int i = 0; i < puzzleSize; i++) {
			boxes[i].GetComponentInChildren<Text>().text = puzzle[i].ToString();
		}

		answerKey = new GameObject[puzzleSize];
		realign();
		for(int i = 0; i < currentPuz.Length; i++) {
			answerKey[i] = currentPuz[i];
		}
		// answerKey = currentPuz;
		mixEmUp();
			
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		if(StateManager.instance != null && StateManager.instance.currentState == GameStates.Play) {

		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!GetComponent<Animation>().isPlaying && !gameStart) {
			gameStart = true;
		}

		if(gameStart) {
			if(!mixed) {
				timer += Time.deltaTime;
				if(!mousedown.draggingItem && !mousedown.reorderPuz) {
					alignGrid();
					if(GameWon(currentPuz, answerKey)) {
						Debug.Log("WINNER!");
						GameManager.instance.RemoveFromPool(this.gameObject, true);
					}


					if(timer > 50) {
						//TODO : YOU LOSE
					}

				}


				if(mousedown.reorderPuz) {
					// REORDER THE PUZZLE BASED ON MOUSEDOWN
					realign();
					OutOfPlace();
					mousedown.reorderPuz = false;

				}

			}
		}

	}


	void alignGrid() {
		GameObject[] holder = new GameObject[puzzleSize];
 		for(int i = 0; i < puzzleSize; i++) {
			 for(int x = 0; x < puzzleSize; x++) {
				if(V2Equal((Vector2)boxes[i].transform.position, startPositions[x])) {
					holder[x] = boxes[i];
				}
			 }
		}
		currentPuz = holder;

		puzDelim.Clear();
		foreach(GameObject box in currentPuz) {
			puzDelim.Add(box);
		}

	}


	void realign() {
		GameObject[] holder = new GameObject[puzzleSize];
 		for(int i = 0; i < puzzleSize; i++) {
			 for(int x = 0; x < puzzleSize; x++) {
				if(V2Equal((Vector2)boxes[i].transform.position, startPositions[x])) {
					holder[x] = boxes[i];
				}
			 }
		}
		currentPuz = holder;

	}

	void OutOfPlace() {
		int indexOfoutofPlace = -1;
		for(int i = 0; i < currentPuz.Length; i++) {
			if(currentPuz[i] == null) {
				indexOfoutofPlace = i;
			}
		}


		if(indexOfoutofPlace != -1) {
			int indOfOOPBOX = -1;
			for(int i = 0; i < puzDelim.Count; i++) {
				if(puzDelim[indexOfoutofPlace] == boxes[i]) {
					indOfOOPBOX = i;
				}
			}




			float diffY = Mathf.Abs(boxes[indexOfoutofPlace].transform.position.y - startPositions[0].y);
			
			if(diffY > 100) {
				boxes[indOfOOPBOX].transform.position = startPositions[indexOfoutofPlace];
			} else {
				List<float> distance = new List<float>();

				for(int i = 0; i < puzzleSize; i++) {
					distance.Add(Mathf.Abs(boxes[indOfOOPBOX].transform.position.x - startPositions[i].x));
				}

				float[] order = new float[distance.Count];

				for(int i = 0; i < distance.Count; i++) {
					order[i] = distance[i];
				}
				distance.Sort();

				int indOfnewPos = -1;
				for(int i = 0; i < order.Length; i++) {
					if(order[i] == distance[0]) {
						indOfnewPos = i;
					}
				}

				if(currentPuz[indOfnewPos] != null) {
					currentPuz[indOfnewPos].transform.position = startPositions[indexOfoutofPlace];
				}

				boxes[indOfOOPBOX].transform.position = startPositions[indOfnewPos];

			}
		
		}

	}

	public bool V2Equal(Vector2 a, Vector2 b){
		return Vector2.SqrMagnitude(a - b) < 0.0001;
	}

	bool GameWon(GameObject[] a, GameObject[] b) {
		bool retVal = true;

		for(int i = 0; i < a.Length; i++) {
			if(a[i] != b[i]) {
				retVal = false;
			}
		}
		
		return retVal;
	}


	void mixEmUp() {

		do {
			List<int> used = new List<int>();

		
			for(int i = 0; i < puzzleSize; i++) {
				int x = 0;	
				do {
					x = Random.Range(0, puzzleSize);
				} while (used.Contains(x));

				boxes[i].transform.position = startPositions[x];
				used.Add(x);
			}
			realign();
			//TODO: THIS COULD BE AN INFINITE LOOPS
			if(!GameWon(currentPuz, answerKey)) {
				mixed = false;
			}

		}while(mixed);



 	}

}
