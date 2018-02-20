using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MAFFSIGNS { ADD, SUBTRACT, MULTIPLY, DIVIDE };

public class QuickMaff : MonoBehaviour {

	public Text answerT;
	public Text firstSide;
	public Text secondSide;
	public MAFFSIGNS answerKey;
	public int[] primeNumber = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97};
 	public GameObject[] signStarts;
	public Vector2[] startPositions;
	MaffMovement maff;
	public GameObject SIGN_ANSWER;

	bool gameOver = false;
	float timer = 0f;


	// Use this for initialization
	void Start () {
		maff = GetComponent<MaffMovement>();
		startPositions = new Vector2[signStarts.Length];

		for(int i = 0; i < signStarts.Length; i++) {
			startPositions[i] = signStarts[i].transform.position;
		}

		int actualAnswer = -1;
		int leftside_a = -1;
		int rightside_a = 1; 

		answerKey = (MAFFSIGNS)Random.Range(0,4);

		int answer = -1;

		switch(answerKey) {
			case MAFFSIGNS.ADD:
				answer = Random.Range(0,100);
				leftside_a = Random.Range(1, answer);
				rightside_a = answer - leftside_a;
				actualAnswer = answer;
				break;
			case MAFFSIGNS.SUBTRACT:
				answer = Random.Range(0,50);
				leftside_a = Random.Range(answer+1, answer * 2);
				rightside_a = leftside_a - answer;
				actualAnswer = answer;
				break;
			case MAFFSIGNS.DIVIDE:
				answer = Random.Range(11,25);
				rightside_a = Random.Range(0, 10);
				leftside_a = answer * rightside_a;
				actualAnswer = answer;
				break;
			case MAFFSIGNS.MULTIPLY:
				do{
					answer = Random.Range(0,100);
				} while(CalcIsPrime(answer));
				
				do {
					leftside_a = Random.Range(2,10);
				} while(answer % leftside_a != 0);

					rightside_a = answer / leftside_a;
				break;
		}

		answerT.text = answer.ToString();
		secondSide.text = rightside_a.ToString();
		firstSide.text = leftside_a.ToString();
	}


	// Update is called once per frame
	void Update () {
		if(!gameOver) {
			timer += Time.deltaTime;

			if(timer > 10f) {
				// GameManager.instance.RemoveFromPool(this.gameObject, false);
			}

			if(maff.reorderPuz) {
				if(realign()) {
					DidWeWin();
				} 
			}	
		}
	}


	void DidWeWin() {
		int outOfPosition = -1;

		for(int i = 0; i < signStarts.Length; i++) {
			if(!V2Equal(signStarts[i].transform.position, startPositions[i])) {
				outOfPosition = i;
			}
		}

		if(answerKey == (MAFFSIGNS)outOfPosition){
			// GameManager.instance.RemoveFromPool(this.gameObject, true);
		} else {
			// GameManager.instance.RemoveFromPool(this.gameObject, false);
		}

	}


	bool realign() {
		bool retVal = false;
		int outOfPosition = -1;

		for(int i = 0; i < signStarts.Length; i++) {
			if(!V2Equal(signStarts[i].transform.position, startPositions[i])) {
				outOfPosition = i;
			}
		}


		if(V2EqualB(signStarts[outOfPosition].transform.position, SIGN_ANSWER.transform.position)) {
			signStarts[outOfPosition].transform.position = SIGN_ANSWER.transform.position;
			maff.gameOver = true;
			gameOver = true;
			retVal = true;
		} else {
			signStarts[outOfPosition].transform.position = startPositions[outOfPosition];
		}
		maff.reorderPuz = false;
		return retVal;
	}

	bool V2Equal(Vector2 a, Vector2 b) {
		return Vector2.SqrMagnitude(a - b) < 0.0001;
	}

	
	bool V2EqualB(Vector2 a, Vector2 b) {
		return Mathf.Abs(a.magnitude - b.magnitude) < 10;
	}


	public bool CalcIsPrime(int number) {
	
		if (number == 1) return false;
		if (number == 2) return true;
	
		if (number % 2 == 0) return false; // Even number     
	
		for (int i = 2; i < number; i++) { // Advance from two to include correct calculation for '4'
			if (number % i == 0) return false;
		}
	
		return true;
	
	}
 }
