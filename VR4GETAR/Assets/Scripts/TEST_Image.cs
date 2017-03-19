using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Image : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().material.mainTexture = imageToTexture("Assets/Data/testImage.jpg");
		
	}

    private Texture2D imageToTexture(string path)
    {
        Texture2D texture = new Texture2D(2, 2);
        byte[] imageData = System.IO.File.ReadAllBytes(path);
        texture.LoadImage(imageData);

        return texture;
    }
}
