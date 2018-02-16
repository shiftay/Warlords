using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBtn : MonoBehaviour {
	public Color onC = Color.white;
	public Color offC = Color.black;

	public bool on = false;

	public void TURNON() {

			this.on = true;
			GetComponent<Image>().color = onC;

	}

	public void TURNOFF() {

			this.on = false;
			GetComponent<Image>().color = offC;

	}
	
}
