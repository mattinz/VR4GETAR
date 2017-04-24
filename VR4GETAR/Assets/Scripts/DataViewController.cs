using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataViewController : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject image;

    public string extra;

	// Use this for initialization
	void Start ()
    {
        //text.SetActive(false);
        //image.SetActive(false);
	}

    private void Update()
    {
        //This is a hack to ensure that the data view remains kinematic after being dropped.
        //I think that the beter way would be to do it using the OnDetachFromHand UnityEvent, but that doesn't seem to be working.
        if(GetComponent<Rigidbody>() != null && !GetComponent<Rigidbody>().isKinematic)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public bool isInteractable()
    {
        return GetComponent<Valve.VR.InteractionSystem.Throwable>() != null;
    }

    public void makeInteractable()
    {
        gameObject.AddComponent<Valve.VR.InteractionSystem.Throwable>();
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Valve.VR.InteractionSystem.Throwable>().restoreOriginalParent = true;
        UnityEvent onDetachFromHand = new UnityEvent();
        //onDetachFromHand.AddListener(makeKinematic);
        GetComponent<Valve.VR.InteractionSystem.Throwable>().onDetachFromHand = onDetachFromHand;
        
    }

    /*void makeKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }*/

    public void setText(string data, string raw)
    {
        text.SetActive(true);
        image.SetActive(false);

        extra = raw;
        text.GetComponent<TextMesh>().text = data;
    }

    public void setImage(Texture2D data, string title)
    {
        text.SetActive(false);
        image.SetActive(true);

        extra = title;
        image.GetComponent<Renderer>().material.mainTexture = data;
    }
}
