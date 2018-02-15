using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ROTSTAGES { FIRST, SECOND, THIRD, FOURTH };
public class Balance : MonoBehaviour {

	public int[] timerSteps = { 5 , 10, 15, 20 };
	public float[] rotateStages =  { 5, -10, 15, -25 };
	public float[] velocityStages = { 15, 25, 40 };
	public GameObject bar;
	public BarRotation barRot;
	public GameObject circle;
	public Vector2 circleStart;
	public float timer = 0f;
	public float rotateTimer = 0f;
	public float steps = 1f;
	ROTSTAGES currentStage;
	bool rotate;


	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()	{
		circleStart = circle.transform.position;
		timer = 0f;
		barRot = bar.GetComponent<BarRotation>();
		currentStage = ROTSTAGES.FIRST;
	}


	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(!rotate) {
			rotateTimer += Time.deltaTime;
		}


		capVelocity();


		if(!barRot.draggingItem) {
			if(rotateTimer > timerSteps[(int)currentStage]) {
				rotate = true;
				// bar.GetComponent<Rigidbody2D>().rotation += steps;
				bar.transform.rotation = Quaternion.RotateTowards(bar.transform.rotation, Quaternion.Euler(0,0,rotateStages[(int)currentStage]), steps);
				// rotateTimer = 0;

				if(QuaEQUALS(bar.transform.rotation, Quaternion.Euler(0,0,rotateStages[(int)currentStage]))) {
					rotate = false;
					rotateTimer = 0f;
					currentStage++;
					if(currentStage == ROTSTAGES.FOURTH) {
						Debug.Log("GAME OVER!");
					}
				}
			}
		}




	}


	bool QuaEQUALS(Quaternion a, Quaternion b) {
		return Quaternion.Angle(a, b) < 2;
	}


	void capVelocity() 	{
		if(Mathf.Abs(circle.GetComponent<Rigidbody2D>().velocity.x) > 15 ) {
			Vector2 hldr = circle.GetComponent<Rigidbody2D>().velocity;

			hldr.x = Mathf.Sign(hldr.x) * 15;

			circle.GetComponent<Rigidbody2D>().velocity = hldr;

		}
		
		if (Mathf.Abs(circle.GetComponent<Rigidbody2D>().velocity.y) > 15) {
			Vector2 hldr = circle.GetComponent<Rigidbody2D>().velocity;

			hldr.y = Mathf.Sign(hldr.y) * 15;

			circle.GetComponent<Rigidbody2D>().velocity = hldr;

		}
	}
}
