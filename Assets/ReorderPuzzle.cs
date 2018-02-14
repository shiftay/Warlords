using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorderPuzzle : MonoBehaviour {

	public GameObject[] boxes;
	public Vector2[] startPositions;
	public MousedownScript mousedown;

	// Use this for initialization
	void Start () {
		startPositions = new Vector2[] { boxes[0].transform.position, boxes[1].transform.position, boxes[2].transform.position, boxes[3].transform.position };
	}
	
	// Update is called once per frame
	void Update () {
		if(!mousedown.draggingItem) {




			
		}





	}
}
