﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject dataDisplay;
    [SerializeField] private GameObject laserAttachPoint;
    [SerializeField] private float laserThickness = 0.01f;
    [SerializeField]private Color laserDefaultColor = Color.red;
    [SerializeField]private Color laserHitColor = Color.green;

    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject laser;
    private Material laserDefaultMaterial;
    private Material laserHitMaterial;
    private bool showingData;

    void Awake()
    {
        hand = GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    // Use this for initialization
    void Start ()
    {
        laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(laser.GetComponent<BoxCollider>());

        laser.transform.SetParent(laserAttachPoint.transform);
        laser.transform.localScale = new Vector3(laserThickness, laserThickness, 0.0f);
        laser.transform.localPosition = Vector3.zero;
        laser.transform.localRotation = Quaternion.identity;

        laserDefaultMaterial = new Material(Shader.Find("Unlit/Color"));
        laserDefaultMaterial.SetColor("_Color", laserDefaultColor);

        laserHitMaterial = new Material(Shader.Find("Unlit/Color"));
        laserHitMaterial.SetColor("_Color", laserHitColor);

        laser.GetComponent<MeshRenderer>().material = laserDefaultMaterial;
        laser.SetActive(false);

        showingData = false;
    }
	
	void Update ()
    {
        if (showingData)
        {
            laser.SetActive(false);
            checkDataDisplay();
        }
        else
        {
            checkLaser();
        }

        
	}

    private void checkDataDisplay()
    {
        if(hand.GetButtonDown(SteamVR_Controller.ButtonMask.Grip))
        {
            dataDisplay.GetComponent<DataDisplayController>().clear();
            showingData = false;
        }
        else if(hand.GetStandardInteractionButtonDown())
        {
            dataDisplay.GetComponent<DataDisplayController>().next();
        }
        else if(hand.GetButtonDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            GameObject dataView = dataDisplay.GetComponent<DataDisplayController>().getCurrentDataView();
            dataView.GetComponent<DataViewController>().makeInteractable();

            GameObject collection = GameObject.FindGameObjectWithTag("Collection");
            if(collection != null)
            {
                dataView.transform.parent = collection.transform;
            }
        }
    }

    private void checkLaser()
    {
        laser.SetActive(false);
        laser.GetComponent<MeshRenderer>().material = laserDefaultMaterial;

        if (hand.GetButton(SteamVR_Controller.ButtonMask.Grip))
        {
            RaycastHit hit;
            if (Physics.Raycast(laserAttachPoint.transform.position, laserAttachPoint.transform.forward, out hit))
            {
                laser.transform.position = laserAttachPoint.transform.position + laserAttachPoint.transform.forward * hit.distance / 2;
                laser.transform.localScale = new Vector3(laserThickness, laserThickness, hit.distance);
                laser.SetActive(true);

                //Lots of code duplication here. If it gets worse, abstract it out.
                if (hit.collider.transform.GetComponent<PointController>() != null)
                {
                    laser.GetComponent<MeshRenderer>().material = laserHitMaterial;
                    if (hand.GetStandardInteractionButtonDown())
                    {
                        List<IData> hitData = hit.collider.transform.GetComponent<PointController>().getData();
                        dataDisplay.GetComponent<DataDisplayController>().show(hitData);
                        showingData = true;
                    }
                }
                else if (hit.transform.GetComponent<DataViewController>() != null
                    && hit.transform.GetComponent<DataViewController>().isInteractable())
                {
                    laser.GetComponent<MeshRenderer>().material = laserHitMaterial;
                    if (hand.GetStandardInteractionButtonDown())
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
                else if(hit.transform.parent != null 
                    && hit.transform.parent.gameObject.GetComponent<Menu>() != null)
                {
                    laser.GetComponent<MeshRenderer>().material = laserHitMaterial;
                    if (hand.GetStandardInteractionButtonDown())
                    {
                        hit.transform.parent.GetComponent<Menu>().buttonPressed(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
