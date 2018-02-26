using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PaddleRotation { FIRST, SECOND, THIRD }
public class BreakOut : MonoBehaviour {

	public TopBorder hitCounter;
	public Vector2 startPos;
	public GameObject ball;
	public GameObject paddle;
	PaddleRotation currentRotation;
	Quaternion firstRot = Quaternion.Euler(0,0,180);
	Quaternion secondRot = Quaternion.Euler(0,0,135);
	bool rotated = false;

	public bool gameStarted = false;


	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()	{
		currentRotation = PaddleRotation.FIRST;
		startPos = ball.transform.position;
		hitCounter.hitCounter = -1;
		// GameObject test = Instantiate(ballPrefab, startPos, Quaternion.identity);
		// test.transform.parent.SetParent(this.transform);
	}

	// Update is called once per frame
	void Update () {
		if(!GetComponent<Animation>().isPlaying && !gameStarted) {
			gameStarted = true;
			ball.GetComponent<Ball>().startBall();
		} 

		if(gameStarted) {
			switch(currentRotation) {
				case PaddleRotation.FIRST:

						if(hitCounter.hitCounter >= 3) {
							paddle.transform.rotation = Quaternion.RotateTowards(paddle.transform.rotation, firstRot, 5f);
							if(paddle.transform.rotation.eulerAngles.z == 180) {
								currentRotation = PaddleRotation.SECOND;
							}
						}
				break;
				case PaddleRotation.SECOND:
						if(hitCounter.hitCounter >= 6) {
							paddle.transform.rotation = Quaternion.RotateTowards(paddle.transform.rotation, secondRot, 5f);
							if(paddle.transform.rotation.eulerAngles.z == 135) {
								currentRotation = PaddleRotation.THIRD;
							}
						}
				break;
				case PaddleRotation.THIRD:
						if(hitCounter.hitCounter >= 9 && !rotated) {
							// TODO: puzzle completed.
							GameManager.instance.RemoveFromPool(this.gameObject, true);
							rotated = true;
						}
				break;
			}

		}
		// TODO: Change stages to be based off a single number





		// if(hitCounter.hitCounter >= 3 && !rotated) {
		// 	paddle.transform.rotation = Quaternion.RotateTowards(paddle.transform.rotation, Quaternion.Euler(0,0,180), 15f);
		// 	if(paddle.transform.rotation.eulerAngles.z == 180) {
		// 		rotated = true;
		// 	}
		// }



		if(!ball.activeInHierarchy) {
			//TODO: GAMEOVER
			GameManager.instance.RemoveFromPool(this.gameObject, false);
		}
	}


	
}
