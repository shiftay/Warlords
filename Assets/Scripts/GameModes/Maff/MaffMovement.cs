using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaffMovement : MonoBehaviour {

 public bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;
	public bool reorderPuz = false;
	public Vector2 droppedPos;
	public bool gameOver = false;


    void Update ()
    {
		if(!gameOver) {
			if (HasInput){
				DragOrPickUp();
			} else {
				if (draggingItem)
					DropItem();
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
            draggedObject.transform.position = inputPosition + touchOffset;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null) {
                    if(hit.transform.tag == "Sign_MAFF") {
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

}
