﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataViewController : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject image;

	// Use this for initialization
	void Start ()
    {
        //text.SetActive(false);
        //image.SetActive(false);
	}

    public bool isInteractable()
    {
        return GetComponent<Valve.VR.InteractionSystem.Throwable>() != null;
    }

    public void makeInteractable()
    {
        gameObject.AddComponent<Valve.VR.InteractionSystem.Throwable>();
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void setText(string data)
    {
        text.SetActive(true);
        image.SetActive(false);

        text.GetComponent<TextMesh>().text = data;
    }

    public void setImage(Texture2D data)
    {
        text.SetActive(false);
        image.SetActive(true);

        image.GetComponent<Renderer>().material.mainTexture = data;
    }
}
