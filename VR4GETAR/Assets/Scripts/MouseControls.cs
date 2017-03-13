using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    private PointController currentController;

	// Use this for initialization
	void Start ()
    {
        currentController = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform.GetComponent<PointController>())
                {
                    //Close currently open point before opening newly selected one.
                    if(currentController != null)
                    {
                        currentController.onSelect();
                    }

                    currentController = hit.collider.transform.GetComponent<PointController>();
                    currentController.onSelect();
                }
            }
        }

        float horiz = -1 * Input.GetAxis("Horizontal");
        if (horiz != 0.0f && currentController != null)
        {
            currentController.onSelect();
            currentController = null;
        }
        transform.Rotate(transform.up, 45 * horiz * Time.deltaTime);
	}
}
