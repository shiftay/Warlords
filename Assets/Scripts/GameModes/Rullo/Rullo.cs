using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rullo : MonoBehaviour {

	public Button[] texts;
	public Text[] rowText;
	public Text[] colText;

	private Text[,] currVals;
	bool[,] puzzleKey = new bool[3,3];
	bool gameStart = false;
	public float timer = 0f;
	public List<RulloBtn> btns = new List<RulloBtn>();

	public Image[] completedRows;


	// Use this for initialization
	void Start () {
		currVals = new Text[3,3];
		int x = 0;
		int y = 0;
		for(int i = 0; i < texts.Length; i++) {

			btns.Add(texts[i].GetComponent<RulloBtn>());

			if(i % 3 == 0 && i != 0) {
				x++;
				y = 0;
			}
			currVals[x,y] = texts[i].GetComponentInChildren<Text>();

			y++;
		}


		CreatePuzzle();
		
		

	}
	
	// Update is called once per frame
	void Update () {
		if(!GetComponent<Animation>().isPlaying && !gameStart) {
			gameStart = true;
		}

		if(gameStart) {
			timer += Time.deltaTime;

			updateBoxes();

			if(GameWon()) {
				GameManager.instance.RemoveFromPool(this.gameObject, true);
			}


			if(timer > 20f) {
				// GameManager.instance.RemoveFromPool(this.gameObject, false);
			}
		}
	}



	bool GameWon() {
		bool retVal = true;

		int x = 0, y = 0;

		for(int i = 0; i < btns.Count; i++) {
			if(i % 3 == 0 && i != 0) {
				x++;
				y = 0;
			}
			
			if(puzzleKey[x,y] != btns[i].on) {
				retVal = false;
			}


			y++;

			
		}

		return retVal;
	}


	void updateBoxes() {
		//ROWS
		completedRows[0].enabled = checkRow(0, 0);
		completedRows[1].enabled = checkRow(0, 0);
		completedRows[2].enabled = checkRow(3, 1);
		completedRows[3].enabled = checkRow(3, 1);
		completedRows[4].enabled = checkRow(6, 2);
		completedRows[5].enabled = checkRow(6,2);
		//COLS
		completedRows[6].enabled = checkCol(0, 0);
		completedRows[7].enabled = checkCol(0, 0);
		completedRows[8].enabled = checkCol(1, 1);
		completedRows[9].enabled = checkCol(1, 1);
		completedRows[10].enabled = checkCol(2, 2);
		completedRows[11].enabled = checkCol(2, 2);
	}

	bool checkRow(int i, int row){ 
		return (btns[i].on == puzzleKey[row,0] && btns[i+1].on == puzzleKey[row,1] && btns[i+2].on == puzzleKey[row,2]);
	}

	bool checkCol(int i, int col) {
		return(btns[i].on == puzzleKey[0,col] && btns[i+3].on == puzzleKey[1,col] && btns[i+6].on == puzzleKey[2,col]);
	}

	void CreatePuzzle() {
		int[,] hldr = new int[3,3];
		List<int> usedX = new List<int>();
		List<int> usedY = new List<int>();
		int[] colVals = new int[3];
		int[] rowVals = new int[3];

		int[] strCol = new int[3];
		int[] strRow = new int[3];

		bool resetRow = false;
		for(int x = 0; x < 3; x++) {
			
			do {
				resetRow = false;
				for(int y = 0; y < 3; y++) {
					puzzleKey[x,y] = Random.Range(0,10) > 6 ? false : true;
				}

				if((!puzzleKey[x,0] && !puzzleKey[x,1] && !puzzleKey[x,2])) {
					resetRow = true;
				}

				if((puzzleKey[x,0] && puzzleKey[x,1] && puzzleKey[x,2])) {
					resetRow = true;
				}

			} while(resetRow);
		}

		for(int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++) {
				hldr[i,j] = Random.Range(2,9);
				rowVals[i] += puzzleKey[i,j] ? hldr[i,j] : 0;
			}
		}

		for(int x = 0; x < 3; x++) {
			for(int l = 0; l < 3; l++) {
				colVals[x] += puzzleKey[l,x] ? hldr[l,x] : 0;
			}
		}

		// colVals[0] = hldr[0,0] + hldr[1,0] + hldr[2,0];
		// colVals[1] = hldr[0,1] + hldr[1,1] + hldr[2,1];
		// colVals[2] = hldr[0,2] + hldr[1,2] + hldr[2,2];


		for(int a = 0; a <3; a++) {
			for(int g = 0; g <3; g++) {
				currVals[a,g].text = hldr[a,g].ToString();
			}
		}

		int z = 0;

		for(int b = 0; b < 6; b+=2) {
			colText[b].text = colVals[z].ToString();
			colText[b+1].text =	colVals[z].ToString();
			rowText[b].text = rowVals[z].ToString();
			rowText[b+1].text = rowVals[z].ToString();
			z++;
		}

	}


	public void ToggleThis(GameObject btn) {
		int indexOfbtn = btns.IndexOf(btn.GetComponent<RulloBtn>());

		if(btn.GetComponent<RulloBtn>().on) {
			btn.GetComponent<RulloBtn>().TURNOFF();
		} else {
			btn.GetComponent<RulloBtn>().TURNON();
		}
		// int x = indexOfbtn / 3;
		// int y = indexOfbtn % 3;




	}

	// void toggle(int x, int y) {
	// 	if(x >= 0 && x < 3 && y >= 0 && y < 3) {
	// 		if (lights2[x,y].on ) {
	// 			lights2[x,y].TURNOFF();
	// 		} else {
	// 			lights2[x,y].TURNON();
	// 		}
	// 	} 
	// }
}
