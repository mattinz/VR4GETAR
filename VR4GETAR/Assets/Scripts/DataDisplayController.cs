using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDisplayController : MonoBehaviour
{
    private IData[] dataArray;
    private int currentIndex;

    public void clear()
    {
        deleteCurrentDataView();
        dataArray = null;
    }

    public GameObject getCurrentDataView()
    {
        Transform currentView = transform.GetChild(0);
        GameObject clone = null;

        if (currentView != null)
        {
            clone = Instantiate(currentView.gameObject, null);
            clone.transform.position = currentView.transform.position;
            clone.transform.rotation = currentView.transform.rotation;
        }

        return clone;
    }

    public void next()
    {
        if (dataArray != null)
        {
            deleteCurrentDataView();
            currentIndex = (currentIndex + 1) % dataArray.Length;
            showDataView(dataArray[currentIndex]);
        }
    }

    public void show(List<IData> data)
    {
        dataArray = data.ToArray();
        currentIndex = 0;

        showDataView(dataArray[currentIndex]);
    }

    private void deleteCurrentDataView()
    {
        Transform currentView = transform.GetChild(0);
        if (currentView != null)
        {
            Destroy(currentView.gameObject);
        }
    }

    private void showDataView(IData data)
    {
        GameObject dataView = data.createDataView();
        dataView.transform.parent = transform;
        dataView.transform.localPosition = Vector3.zero;
        dataView.transform.localRotation = Quaternion.identity;
    }
}
