using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine;

public class CollectionController : MonoBehaviour
{

    public void save(string name)
    {
        string output = "";
        foreach(Transform t in transform)
        {
            if (t.gameObject.GetComponent<DataViewController>() != null)
            {
                DataViewController dataView = t.gameObject.GetComponent<DataViewController>();
                output += dataView.extra + ",";
                output += t.position.x + "," + t.position.y + "," + t.position.z + ",";
                output += t.rotation.x + "," + t.rotation.y + "," + t.rotation.z + "," + t.rotation.w + "\n";
                //output += t.position.ToString().Replace(',', ' ') + ",";
                //output += t.rotation.ToString().Replace(',', ' ') + "\n";
            }
        }
        File.WriteAllText(DataLoader.DATA_DIR + "\\" + name + ".col", output);
        Debug.Log(output);
    }

    public void load(string name)
    {
        StreamReader reader = new StreamReader(DataLoader.DATA_DIR + "\\" + name + ".col");
        while(!reader.EndOfStream)
        {
            int offset;
            IData dataObject;
            string[] line = reader.ReadLine().Split(',');
            if(line.Length == 12)
            {
                //Text
                offset = 5;
                string rawData = line[0] + "," + line[1] + "," + line[2] + "," + line[3] + "," + line[4];
                dataObject = DataLoader.parseTextData(rawData);
            }
            else
            {
                //image
                offset = 1;
                dataObject = DataLoader.parseImageData(line[0]);
            }

            //Get Position
            /*string[] posString = line[line.Length - 2].Split(' ');
            float posX = float.Parse(posString[0].Replace('(', ' ').Trim(), System.Globalization.CultureInfo.InvariantCulture);
            float posY = float.Parse(posString[1].Trim());
            float posZ = float.Parse(posString[2].Replace(')', ' ').Trim());
            Vector3 pos = new Vector3(posX, posY, posZ);*/

            //Get rotation
            /*string[] rotString = line[line.Length - 1].Split(' ');
            float rotX = float.Parse(rotString[0].Replace('(', ' ').Trim());
            float rotY = float.Parse(rotString[1].Trim());
            float rotZ = float.Parse(rotString[2].Trim());
            float rotW = float.Parse(rotString[3].Replace(')', ' ').Trim());
            Quaternion rot = new Quaternion(rotX, rotY, rotZ, rotW);*/

            Vector3 pos = new Vector3(float.Parse(line[offset], CultureInfo.InvariantCulture),
                float.Parse(line[offset+1], CultureInfo.InvariantCulture),
                float.Parse(line[offset+2], CultureInfo.InvariantCulture));
            Quaternion rot = new Quaternion(float.Parse(line[offset + 3], CultureInfo.InvariantCulture), 
                float.Parse(line[offset + 4], CultureInfo.InvariantCulture), 
                float.Parse(line[offset + 5], CultureInfo.InvariantCulture), 
                float.Parse(line[offset + 6], CultureInfo.InvariantCulture));

            GameObject dataView = dataObject.createDataView();
            dataView.transform.position = pos;
            dataView.transform.rotation = rot;
            dataView.transform.parent = transform;
            dataView.GetComponent<DataViewController>().makeInteractable();
        }
        reader.Close();
    }

    public void clear()
    {
        foreach(Transform t in transform)
        {
            if(t.gameObject.GetComponent<DataViewController>() != null)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
