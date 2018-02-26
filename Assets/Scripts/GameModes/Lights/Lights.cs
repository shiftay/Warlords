using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lights : MonoBehaviour {

	public GameObject[] buttons;

	public List<LightBtn> lights;

	public LightBtn[,] lights2 = new LightBtn[3,3];

	public float timer = 0f;
	public bool gameStarted = false;

	// Use this for initialization
	void Start () {
		int x = 0;
		int y = 0;
		for(int i = 0; i < buttons.Length; i++) {
			lights.Add(buttons[i].GetComponent<LightBtn>());

			if(i % 3 == 0 && i != 0) {
				x++;
				y = 0;
			}
			lights2[x,y] = buttons[i].GetComponent<LightBtn>();

			y++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!GetComponent<Animation>().isPlaying && !gameStarted) {
			startGame();
			gameStarted = true;
		}

		if(gameStarted) {
			if(gameWon()) {
				GameManager.instance.RemoveFromPool(this.gameObject, true);
			}

			timer += Time.deltaTime;

			if(timer > 50f) {
				GameManager.instance.RemoveFromPool(this.gameObject, false);
			}

		}
	}

	public void ToggleThis(GameObject btn) {
		int indexOfbtn = lights.IndexOf(btn.GetComponent<LightBtn>());

		int x = indexOfbtn / 3;
		int y = indexOfbtn % 3;

		toggle(x,y);
		toggle(x+1,y);
		toggle(x-1, y);
		toggle(x, y+1);
		toggle(x, y-1);
	}

	void toggle(int x, int y) {
		if(x >= 0 && x < 3 && y >= 0 && y < 3) {
			if (lights2[x,y].on ) {
				lights2[x,y].TURNOFF();
			} else {
				lights2[x,y].TURNON();
			}
		} 
	}

	bool gameWon() {
		bool retVal = true;

		for(int i = 0; i < lights.Count; i++) {
			if(!lights[i].on) {
				retVal = false;
			}
		}

		return retVal;
	}

	void startGame() {
		foreach(LightBtn light in lights) {
			light.GetComponent<Button>().interactable = true;


			int j = Random.Range(0, 10);

			if(j < 5) {
				light.TURNON();
			} else {
				light.TURNOFF();
			}
		}
	}
}
