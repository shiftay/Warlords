using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public float speed = 150f;
	// Use this for initialization
	void OnEnable () {
		Vector2 dir = new Vector2(Random.Range(-1f,1f), 1);
		GetComponent<Rigidbody2D>().velocity = dir * speed;
	}
	
	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		// if(other.gameObject.tag == "racket") {
		// 	float x = hit(transform.position, other.transform.position, other.collider.bounds.size.x);

		// 	Vector2 dir = new Vector2(x, 1).normalized;
		// 	GetComponent<Rigidbody2D>().velocity = dir * speed;
		// }
	}

	float hit(Vector2 ballPos, Vector2 racketPos, float racketWidth) {
		return (ballPos.x - racketPos.x) / racketWidth;
	}
}
