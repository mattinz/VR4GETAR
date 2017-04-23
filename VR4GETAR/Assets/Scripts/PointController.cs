using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] private Material imageMaterial;
    [SerializeField] private Material textMaterial;
    
    private List<IData> dataList;
    private bool showingData;

	void Awake ()
    {
        dataList = new List<IData>();
        showingData = false;
	}

    void Update()
    {
        //transform.LookAt(Camera.main.transform);
    }

    public void addData(IData data)
    {
        if(data is ImageData)
        {
            GetComponent<Renderer>().material = imageMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = textMaterial;
        }
        dataList.Add(data);
    }

    public List<IData> getData()
    {
        return dataList;
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
        foreach(IData data in dataList)
        {
            GameObject dataView = data.createDataView();
            dataView.transform.SetParent(transform);
        }

        //Position the data views
        //TEMPORARY DESKTOP-APP POSITIONING
        /*Vector3 basePos = transform.position + transform.right * 1.2f + transform.forward * 1.0f + transform.up * 0.25f;
        foreach(Transform dataView in transform)
        {
            dataView.position = basePos;
            basePos.y -= 0.2f;
        }*/

        Vector3 basePos = transform.parent.position + transform.parent.right * 1.2f + transform.parent.forward * 0.25f + transform.parent.up * 0.25f;
        foreach (Transform dataView in transform)
        {
            dataView.position = basePos;
            basePos.y -= 0.2f;
            basePos.z += 0.1f;
        }
    }

    private void hideData()
    {
        foreach (Transform dataView in transform)
        {
            Destroy(dataView.gameObject);
        }
    }
}
