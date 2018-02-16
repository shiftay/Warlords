using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

	public GameObject[] mazPrefabs;
	float[] potRot = { 0, 90, 180, 270 };
	public GameObject currentMaze;
	public GameObject player;
	public MazeMovement mMove;
	public Vector2 centerPoint;
	public int currentLevel;
	
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		// centerPoint = player.transform.position;
		mMove = player.GetComponent<MazeMovement>();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		if(StateManager.instance != null && StateManager.instance.currentState == GameStates.Play) {
			if(currentMaze) {
				Destroy(currentMaze);
			}
			centerPoint = player.transform.position;
			currentLevel = Random.Range(0, mazPrefabs.Length);
			
			currentMaze = Instantiate(mazPrefabs[currentLevel], centerPoint, Quaternion.Euler(0,0,potRot[Random.Range(0,potRot.Length)]));
			currentMaze.transform.parent = this.transform;
			player.transform.position = currentMaze.transform.GetChild(1).transform.position;
		}
	}


	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(mMove.gameOver) {
			GameManager.instance.RemoveFromPool(this.gameObject);
		}
	}

}
