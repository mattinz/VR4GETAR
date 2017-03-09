using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    [SerializeField] private float radius = 1;

	void Start ()
    {
        List<TextData> dataList = DataLoader.getTextData("Assets/Data");
        foreach(TextData textData in dataList)
        {
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            point.transform.SetParent(transform);
            point.transform.localPosition = Vector3.zero;

            //Rotate to face proper lat lng
            float lat = textData.getLatLng().x;
            float lng = textData.getLatLng().y;
            point.transform.position += Quaternion.AngleAxis(lng, -Vector3.up) * Quaternion.AngleAxis(lat, -Vector3.right) * new Vector3(0, 0, radius);
            //point.transform.position += point.transform.forward * radius;
        }
	}
}
