using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataLoader
{
    public static readonly string DATA_DIR = "Assets/Data";
    private static readonly string LOCATION_FILE_NAME = "Locations.txt";
    private static readonly Dictionary<string, Vector2> locationDictionary = new Dictionary<string, Vector2>();

    static DataLoader()
    {
        //Populate location dictionary
        StreamReader reader = new StreamReader(DATA_DIR + "/" + LOCATION_FILE_NAME);
        while(!reader.EndOfStream)
        {
            string[] line = reader.ReadLine().Split(',');
            string location = line[0];
            float lat = float.Parse(line[1]);
            float lng = float.Parse(line[2]);
            locationDictionary.Add(location, new Vector2(lat, lng));
        }
    }

    public static List<IData> getData()
    {
        List<IData> result = new List<IData>();

        getTextData(DATA_DIR, result);
        getImageData(DATA_DIR, result);
        return result;
    }

    public static IData parseTextData(string rawData)
    {
        string[] line = rawData.Split(',');

        if (line.Length == 5)
        {
            string data = line[1] + " - " + line[2];
            float lat = float.Parse(line[3]);
            float lng = float.Parse(line[4]);
            return new TextData(data, lat, lng, rawData);
        }
        else
        {
            return null;
        }
    }

    public static IData parseImageData(string file)
    {
        byte[] imageData = File.ReadAllBytes(file);
        Texture2D image = new Texture2D(1, 1);
        image.LoadImage(imageData);

        string locationName = file.Replace(DATA_DIR + "\\", "").Split('_')[0];
        Vector2 latLng = Vector2.zero;
        locationDictionary.TryGetValue(locationName, out latLng);

        return new ImageData(image, latLng.x, latLng.y, file);
    }

    private static void getTextData(string dir, List<IData> result)
    {
        string[] files = Directory.GetFiles(dir, "*.csv");

        foreach(string file in files)
        {
            StreamReader reader = new StreamReader(file);
            while(!reader.EndOfStream)
            {
                /*string rawData = reader.ReadLine();
                string[] line = rawData.Split(',');

                if (line.Length == 5)
                {
                    string data = line[1] + " - " + line[2];
                    float lat = float.Parse(line[3]);
                    float lng = float.Parse(line[4]);
                    result.Add(new TextData(data, lat, lng, rawData));
                }*/
                IData data = parseTextData(reader.ReadLine());
                if(data != null)
                {
                    result.Add(data);
                }
            }

            reader.Close();
        }
    }

    private static void getImageData(string dir, List<IData> result)
    {
        string[] files = Directory.GetFiles(dir, "*.jpg");
        foreach(string file in files)
        {
            /*byte[] imageData = File.ReadAllBytes(file);
            Texture2D image = new Texture2D(1, 1);
            image.LoadImage(imageData);

            string locationName = file.Replace(DATA_DIR + "\\", "").Split('_')[0];
            Vector2 latLng = Vector2.zero;
            locationDictionary.TryGetValue(locationName, out latLng);

            result.Add(new ImageData(image, latLng.x, latLng.y, file));*/

            result.Add(parseImageData(file));
        }
    }
}
