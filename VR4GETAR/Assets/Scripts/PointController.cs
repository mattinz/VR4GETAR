﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] private Material open;
    [SerializeField] private Material close;

    private List<IData> dataList;
    private bool showingData;

	void Awake ()
    {
        dataList = new List<IData>();
        showingData = false;
	}

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void addData(IData data)
    {
        dataList.Add(data);
    }

    public void onSelect()
    {
        if(showingData)
        {
            hideData();
        }
        else
        {
            showData();
        }
        showingData = !showingData;
    }

    private void showData()
    {
        GetComponent<Renderer>().material = open;

        foreach(IData data in dataList)
        {
            GameObject dataView = data.createDataView();
            dataView.transform.SetParent(transform);
        }

        //Position the data views
        //TEMPORARY DESKTOP-APP POSITIONING
        Vector3 basePos = transform.position + transform.right * 1.2f + transform.forward * 1.0f + transform.up * 0.25f;
        foreach(Transform dataView in transform)
        {
            dataView.position = basePos;
            basePos.y -= 0.2f;
        }
    }

    private void hideData()
    {
        GetComponent<Renderer>().material = close;
        foreach (Transform dataView in transform)
        {
            Destroy(dataView.gameObject);
        }
    }
}
