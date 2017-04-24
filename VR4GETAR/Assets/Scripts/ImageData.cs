using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageData : IData
{
    private Vector2 latLng;
    private Texture2D image;
    private string title;

    public ImageData(Texture2D data, float lat, float lng, string title)
    {
        image = data;
        latLng = new Vector2(lat, lng);
        this.title = title;
    }

    public GameObject createDataView()
    {
        GameObject dataView = GameObject.Instantiate(GameObject.FindGameObjectWithTag("OriginalDataView"));
        dataView.GetComponent<DataViewController>().setImage(image, title);
        dataView.SetActive(true);
        return dataView;
    }

    public Vector2 getLatLng()
    {
        return latLng;
    }

    public string getTitle()
    {
        return title;
    }
}
