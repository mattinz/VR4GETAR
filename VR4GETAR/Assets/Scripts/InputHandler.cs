using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject laserAttachPoint;
    [SerializeField] private float laserThickness = 0.01f;
    [SerializeField]private Color laserDefaultColor = Color.red;
    [SerializeField]private Color laserHitColor = Color.green;

    private Valve.VR.InteractionSystem.Hand hand;
    private GameObject laser;
    private Material laserDefaultMaterial;
    private Material laserHitMaterial;
    

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(hand.GetButton(SteamVR_Controller.ButtonMask.Grip))
        {
            RaycastHit hit;
            if(Physics.Raycast(laserAttachPoint.transform.position, laserAttachPoint.transform.forward, out hit))
            {
                laser.transform.position = laserAttachPoint.transform.position + laserAttachPoint.transform.forward * hit.distance / 2;
                laser.transform.localScale = new Vector3(laserThickness, laserThickness, hit.distance);
                laser.SetActive(true);

                if(hit.collider.transform.GetComponent<PointController>() != null)
                {
                    laser.GetComponent<MeshRenderer>().material = laserHitMaterial;
                    if(hand.GetStandardInteractionButtonDown())
                    {
                        hit.collider.transform.GetComponent<PointController>().onSelect();
                    }
                }
                else
                {
                    laser.GetComponent<MeshRenderer>().material = laserDefaultMaterial;
                }
            }
            else
            {
                laser.SetActive(false);
            }
        }
        else
        {
            laser.SetActive(false);
        }
	}
}
