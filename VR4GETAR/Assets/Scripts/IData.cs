using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData
{
    Vector2 getLatLng();
    GameObject createDataView();
}
