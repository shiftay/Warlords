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
	public float horizontalScrollSpeed = 0.25f;
    public float verticalScrollSpeed = 0.25f;
    private bool scroll = true;
	public RawImage _renderer;

    public void DoActivateTrigger()
    {
        scroll = !scroll;
    }

	void Start () {
		_renderer = GetComponent<RawImage>();
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
			//TODO: Check the UV Rect x / y to make sure we aren't going the wrong way.
		}

		// Rect hldr = _renderer.uvRect;
		// hldr.x += (0.05f * Time.deltaTime) * dir[curDir].x;
		// hldr.y += (0.05f * Time.deltaTime) * dir[curDir].y;
		//  _renderer.uvRect = hldr;

		bg.transform.Translate(dir[curDir]);

	}
}
