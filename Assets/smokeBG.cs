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
		bg = GetComponent<Image>();

		_renderer = GetComponent<RawImage>();
		dir = new Vector2[] { new Vector2(speed,speed), new Vector2(-speed,-speed), new Vector2(speed, -speed), new Vector2(-speed, speed)};
		// dir[0] = 
		curDir = Random.Range(0, dir.Length);

	}
	
	// Update is called once per frame
	void Update () {


		timer += Time.deltaTime;

		if(timer > changeDir) {
			
			
			
			curDir = chooseDir();
			
			// curDir = Random.Range(0, dir.Length);
			timer = 0;
			//TODO: Check the UV Rect x / y to make sure we aren't going the wrong way.




		}

		Rect hldr = _renderer.uvRect;
		hldr.x += (0.05f * Time.deltaTime) * dir[curDir].x;
		hldr.y += (0.05f * Time.deltaTime) * dir[curDir].y;
		 _renderer.uvRect = hldr;

		// bg.transform.Translate(dir[curDir]);

	}

	int chooseDir() {
		int retVal = -1;
		//475.5,267.5
		Rect hldr = _renderer.uvRect;

		if(hldr.x >= 0.2) {
			test.x = -1;
		} else if(hldr.x <= -0.2) {
			test.x = 1;
		}

		if(hldr.y >= 0.2) {
			test.y = -1;
		} else if( hldr.y <= -0.2) {
			test.y = 1;
		}
	
		if(test.x == 0 && test.y == 0) {
			retVal = Random.Range(0, dir.Length);
		} else {
			
			for(int i = 0; i < dir.Length; i++) {
				if(test == dir[i]) {
					retVal = i;
				}
			}

			if(retVal == -1) {
				if(test.x == 0 && test.y != 0) {
					if(test.y == 1) {
						retVal = 3;
					} else {
						retVal = 2;
					}
				} else if(test.x != 0 && test.y == 0) {
					if(test.x == 1) {
						retVal = 0;
					} else {
						retVal = 1;
					}
				}

			}
		}

		return retVal;
	}

}
