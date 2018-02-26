using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 150f;
	// Use this for initialization
	// void OnEnable () {
	// 	Vector2 dir = new Vector2(Random.Range(-1f,1f), 1);
	// 	GetComponent<Rigidbody2D>().velocity = dir * speed;
	// }
	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)	{
		if(other.gameObject.tag == "DeadZone") {
			this.gameObject.SetActive(false);
		}
	}

	float hit(Vector2 ballPos, Vector2 racketPos, float racketWidth) {
		return (ballPos.x - racketPos.x) / racketWidth;
	}


	public void startBall() {
		Vector2 dir = new Vector2(Random.Range(-1f,1f), 1);
		GetComponent<Rigidbody2D>().velocity = dir * speed;
	}
}
