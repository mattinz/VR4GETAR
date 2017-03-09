using System.Collections.Generic;
using System.IO;

public static class DataLoader
{
    public static List<TextData> getTextData(string dir)
    {
        List<TextData> result = new List<TextData>();
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

        return result;
    }
}
