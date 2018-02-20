using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smokeBG : MonoBehaviour {

	Image bg;
	Vector2[] dir;
	int curDir;
	float timer = 0f;
	public float speed = 0.25f;
	public float changeDir = 1.5f;
	void Start () {
		dir = new Vector2[] { new Vector2(speed,speed), new Vector2(-speed,-speed), new Vector2(speed, -speed), new Vector2(-speed, speed)};
		// dir[0] = 
		curDir = Random.Range(0, dir.Length);
		bg = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer > changeDir) {
			curDir = Random.Range(0, dir.Length);
			timer = 0;
		}


		




		bg.transform.Translate(dir[curDir]);

	}
}
