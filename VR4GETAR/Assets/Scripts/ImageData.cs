using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageData : IData
{
    private Vector2 latLng;
    private Texture2D image;

    public ImageData(Texture2D data, float lat, float lng)
    {
        image = data;
        latLng = new Vector2(lat, lng);
    }

    public GameObject createDataView()
    {
        GameObject dataView = GameObject.Instantiate(GameObject.FindGameObjectWithTag("OriginalDataView"));
        dataView.GetComponent<DataViewController>().setImage(image);
        dataView.SetActive(true);
        return dataView;
    }

    public Vector2 getLatLng()
    {
        return latLng;
    }
}
