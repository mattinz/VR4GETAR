using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextData
{
    private Vector2 latLng;
    private string text;

    public TextData(string data, float lat, float lng)
    {
        text = data;
        latLng = new Vector2(lat, lng);
    }

    public string getData()
    {
        return text;
    }

    public Vector2 getLatLng()
    {
        return latLng;
    }
}
