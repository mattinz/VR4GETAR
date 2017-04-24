using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextData : IData
{
    private Vector2 latLng;
    private string text;
    private string rawData;

    public TextData(string data, float lat, float lng, string rawData)
    {
        text = data;
        latLng = new Vector2(lat, lng);
        this.rawData = rawData;
    }

    public GameObject createDataView()
    {
        GameObject dataView = GameObject.Instantiate(GameObject.FindGameObjectWithTag("OriginalDataView"));
        dataView.GetComponent<DataViewController>().setText(text, rawData);
        dataView.SetActive(true);
        return dataView;
    }

    public Vector2 getLatLng()
    {
        return latLng;
    }

    public string getRawData()
    {
        return rawData;
    }
}
