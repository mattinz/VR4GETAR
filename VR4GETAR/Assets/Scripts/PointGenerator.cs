using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private float radius = 1;

	void Start ()
    {
        Dictionary<Vector3, GameObject> pointDict = new Dictionary<Vector3, GameObject>();

        List<IData> dataList = DataLoader.getData();
        foreach(IData data in dataList)
        {            
            Vector2 latLng = data.getLatLng();
            GameObject point;
            if (pointDict.ContainsKey(latLng))
            {
                pointDict.TryGetValue(latLng, out point);
            }
            else
            {
                point = GameObject.Instantiate(pointPrefab, transform);
                point.transform.position = transform.position + Quaternion.AngleAxis(latLng.y, -Vector3.up) * Quaternion.AngleAxis(latLng.x, -Vector3.right) * new Vector3(0, 0, radius);
                pointDict.Add(latLng, point);
            }
            point.GetComponent<PointController>().addData(data);
        }
	}
}
