using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataViewController : MonoBehaviour
{
    [SerializeField] private GameObject Text;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setText(string data)
    {
        Text.GetComponent<TextMesh>().text = data;
    }
}
