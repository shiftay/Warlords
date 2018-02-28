using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maze : MonoBehaviour {

	public GameObject[] mazPrefabs;
	float[] potRot = { 0, 90, 180, 270 };
	public GameObject currentMaze;
	public GameObject player;
	public MazeMovement mMove;
	public Vector2 centerPoint;
	public int currentLevel;
	bool startFade = false;
	public float timer;
	const float TIMED = 2f;
	Color fullAlpha;
	float fadeTimer = 0.15f;

	bool gameStart = false;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		// centerPoint = player.transform.position;
		mMove = player.GetComponent<MazeMovement>();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		if(StateManager.instance != null && StateManager.instance.currentState == GameStates.Main) {
			if(currentMaze) {
				Destroy(currentMaze);
			}
			centerPoint = player.transform.position;
			currentLevel = Random.Range(0, mazPrefabs.Length);
			
			currentMaze = Instantiate(mazPrefabs[currentLevel], centerPoint, Quaternion.Euler(0,0,potRot[Random.Range(0,potRot.Length)]));
			currentMaze.transform.parent = this.transform;
			player.transform.position = currentMaze.transform.GetChild(1).transform.position;


			startFade = true;
			Color hldr = player.GetComponent<Image>().color;
			hldr.a = 0;
			player.GetComponent<Image>().color = hldr;
			hldr = currentMaze.GetComponent<Image>().color;
			hldr.a = 0;
			currentMaze.GetComponent<Image>().color = hldr;

		}
	}


	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update() {
		if(startFade) {
			Fade();
		}

		if(gameStart) {
			timer += Time.deltaTime;
			
			if(mMove.gameOver) {
				GameManager.instance.RemoveFromPool(this.gameObject, true);
			}

			if(timer > 20f) {
				GameManager.instance.RemoveFromPool(this.gameObject, false);
			}

		}

	}





	void Fade() {
		// ColorBlock cb = currentButtons[counter].colors;
		// cb.disabledColor = Color32.Lerp(dullColor, vibrantColor, Mathf.PingPong(speed, Time.deltaTime));
		// currentButtons[counter].colors = cb;
		timer += Time.deltaTime;
		if(timer <= (TIMED * 2)) {
			FadeInAlpha(player.GetComponent<Image>());
			FadeInAlpha(currentMaze.GetComponent<Image>());
		}else if(timer > (TIMED * 2)) {
			timer = 0f;
			mMove.gameOver = false;
			gameStart = true;
		}
	}

	void FadeOutAlpha(Image test) {
		fullAlpha = test.color;
		fullAlpha.a -= Time.deltaTime * fadeTimer;
		test.color = fullAlpha;
	}

	void FadeInAlpha(Image test) {
		fullAlpha = test.color;
		fullAlpha.a += Time.deltaTime * fadeTimer;
		test.color = fullAlpha;
	}



}
