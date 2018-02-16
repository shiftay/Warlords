using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeMovement : MonoBehaviour {
    public bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;
	public bool reorderPuz = false;
	public Vector2 droppedPos;
	bool rePickup = false;
	float timer = 0f;
	public float LOCKOUT = 1.5f;
	public bool gameOver = false;
	Image gameImage;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		gameImage = GetComponent<Image>();
	}

    void Update ()
    {
		if(!gameOver) {
			if(rePickup) {
				gameImage.color = Color.red;
				timer += Time.deltaTime;
				if(timer > LOCKOUT) {
					gameImage.color = Color.white;
					timer = 0 ;
					rePickup = false;
				}
			} else {
				if (HasInput)
				{
					
					DragOrPickUp();
				}
				else
				{
					if (draggingItem)
						DropItem();
				}
			}
		}



    }
     
    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Input.mousePosition;
            return inputPos;
        }
    }
 
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
     
        if (draggingItem)
        {
			Vector2 dir = (Vector2)draggedObject.transform.position - (inputPosition + touchOffset);

			float distance = draggedObject.transform.position.magnitude - (inputPosition + touchOffset).magnitude;
			
			Physics2D.Linecast(draggedObject.transform.position, inputPosition + touchOffset);
			RaycastHit2D test = Physics2D.Linecast(draggedObject.transform.position, (inputPosition + touchOffset));
			if(Mathf.Abs(distance) < 15) {
				if(test.transform.gameObject.tag == "Wall_Maze") {
					draggedObject.transform.position = test.normal;
				} else {
					draggedObject.transform.position = Vector2.Lerp(draggedObject.transform.position, inputPosition + touchOffset, 0.25f);
				}
				
			} 


			// draggedObject.transform.position = inputPosition + touchOffset;

			// if()

        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {
					if(hit.transform.gameObject.tag != "Maze") {
						draggingItem = true;
                   		draggedObject = hit.transform.gameObject;
                    	touchOffset = (Vector2)hit.transform.position - inputPosition;
                    	// draggedObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
					}

                }
            }
        }
    }
 
    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }
 
    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector3(1f,1f,1f);
		reorderPuz = true;
		droppedPos = CurrentTouchPosition;
    }

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)	{
		if(other.gameObject.tag == "Wall_Maze") {
			transform.position = transform.position;
			DropItem();
			rePickup = true;
		}
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "End_Maze") {
			Debug.Log("GAME WON");
			gameOver = true;
			DropItem();
		}
	}
}
