using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarRotation : MonoBehaviour {


    public bool draggingItem = false;
    private GameObject draggedObject;
	public Vector2 droppedPos;
	public Vector2 deltaPos;
	public float sensitivity = 0.4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (HasInput) {
            click();
        }
        else {
            if (draggingItem)
                DropItem();
        }
	}

	void click() {
		Vector2 inputPosition = CurrentTouchPosition;
		



		if (draggingItem)
        {

			if(deltaPos != null) {
				float diff = inputPosition.y - deltaPos.y;
				Debug.Log(diff);
				
				// diff = diff * 0.25f;
				draggedObject.transform.rotation = Quaternion.Euler(0,0, draggedObject.transform.rotation.eulerAngles.z + (diff * sensitivity) );

			}

           
        }
        else
        {
		    RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
               	RaycastHit2D hit = touches[0];
                if (hit.transform != null)
                {
                    if(hit.transform.gameObject.tag == "Box_Slider") {
                        draggingItem = true;
                        draggedObject = hit.transform.gameObject;
                        
                    }
                    // draggedObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
                }
            }
		}

		deltaPos = inputPosition;
	}

   void DropItem() {
        draggingItem = false;

        draggedObject.transform.localScale = new Vector3(1f,1f,1f);
		droppedPos = CurrentTouchPosition;
    }

	Vector2 CurrentTouchPosition {
        get {
            Vector2 inputPos;
            inputPos = Input.mousePosition;
            return inputPos;
        }
    }


    private bool HasInput  {
        get {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }
}
