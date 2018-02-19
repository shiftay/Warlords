using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulloBtn : MonoBehaviour {

	public bool on = true;

	public Sprite imgOn;
	public Sprite imgOff;

	public void TURNON() {

			this.on = true;
			GetComponent<Image>().sprite = imgOn;

	}

	public void TURNOFF() {

			this.on = false;
			GetComponent<Image>().sprite = imgOff;

	}
}
