using System.Collections.Generic;
using System.IO;

public static class DataLoader
{
    public static List<IData> getData(string dir)
    {
        List<IData> result = new List<IData>();

        getTextData(dir, result);
        return result;
    }

    private static void getTextData(string dir, List<IData> result)
    {
        string[] files = Directory.GetFiles(dir, "*.csv");

        foreach(string file in files)
        {
            StreamReader reader = new StreamReader(file);
            while(!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');

                if (line.Length == 5)
                {
                    string data = line[1] + " - " + line[2];
                    float lat = float.Parse(line[3]);
                    float lng = float.Parse(line[4]);
                    result.Add(new TextData(data, lat, lng));
                }
            }

            reader.Close();
        }
    }
}
