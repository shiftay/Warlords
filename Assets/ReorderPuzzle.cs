﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorderPuzzle : MonoBehaviour {

	public GameObject[] boxes;
	public Vector2[] startPositions;
	public MousedownScript mousedown;
	public List<GameObject> puzDelim = new List<GameObject>();
	public GameObject[] currentPuz;
	public float timer = 0f;

	// Use this for initialization
	void Start () {
		startPositions = new Vector2[] { boxes[0].transform.position, boxes[1].transform.position, boxes[2].transform.position, boxes[3].transform.position };
		foreach(GameObject box in boxes) {
			puzDelim.Add(box);
		}

	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		timer = 0;
		currentPuz = new GameObject[boxes.Length];
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(!mousedown.draggingItem && !mousedown.reorderPuz) {

			//CHECK IF THEY HAVE WON
			alignGrid();


		}


		if(mousedown.reorderPuz) {
			// REORDER THE PUZZLE BASED ON MOUSEDOWN
			realign();
			OutOfPlace();
			mousedown.reorderPuz = false;

		}


	}


	void alignGrid() {
		GameObject[] holder = new GameObject[boxes.Length];
 		for(int i = 0; i < boxes.Length; i++) {
			 for(int x = 0; x < boxes.Length; x++) {
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
		GameObject[] holder = new GameObject[boxes.Length];
 		for(int i = 0; i < boxes.Length; i++) {
			 for(int x = 0; x < boxes.Length; x++) {
				if(V2Equal((Vector2)boxes[i].transform.position, startPositions[x])) {
					holder[x] = boxes[i];
				}
			 }
		}
		currentPuz = holder;

	}

	void OutOfPlace() {

		//TODO: Wrap this in a if none are null brace.
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

				for(int i = 0; i < startPositions.Length; i++) {
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
		// puzDelim.Clear();
		// foreach(GameObject box in boxes) {
		// 	puzDelim.Add(box);
		// }

	}

	public bool V2Equal(Vector2 a, Vector2 b){
		return Vector2.SqrMagnitude(a - b) < 0.0001;
	}
}