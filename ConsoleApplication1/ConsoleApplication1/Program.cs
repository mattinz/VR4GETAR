using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    class CSVParser
    {
        public static void Main(string[] args)
        {
            LinkedList<DataPoint> dplist = new LinkedList<DataPoint>();
            LinkedListNode<DataPoint> dp;

            //Parse the csv
            using (TextFieldParser parser = new TextFieldParser("Dataset_z_28_200_tweets_lat_long.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Create a new data point based on the fields and append it to the list
                    string[] fields = parser.ReadFields();
                    dplist.AddLast(new DataPoint(fields[0], fields[1], fields[2],
                                Convert.ToDouble(fields[3]), Convert.ToDouble(fields[4])));
                }
            }

            //For Testing
            dp = dplist.First;
            while(dp != null)
            {
                dp.Value.print();
                dp = dp.Next;
            }

            Console.ReadLine();
        }
    }

    class DataPoint
    {
        //Variables
        String date;
        String user;
        String tweet;
        Double latitude;
        Double longitude;

        //Constructor
        public DataPoint(String date, String user, String tweet,
                         Double latitude, Double longitude)
        {
            this.date = date;
            this.user = user;
            this.tweet = tweet;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        //Getter Methods
        public String getDate()
        {
            return date;
        }

        public String getUser()
        {
            return user;
        }

        public String getTweet()
        {
            return tweet;
        }

        public Double getLat()
        {
            return latitude;
        }

        public Double getLong()
        {
            return longitude;
        }

        //For Testing
        public void print()
        {
            Console.WriteLine(date + ", " + user + ", " + tweet + ", ");
            Console.WriteLine(latitude + ", " + longitude);
        }
    }
}
