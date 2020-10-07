using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;

namespace LogAnalyser.IO
{
    class JsonReader
    { 
        public static List<DataPoint> Read(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            var strList = JsonSerializer.Deserialize<List<string>>(jsonString);

            List<DataPoint> points = new List<DataPoint>();

            strList.ForEach(item =>
            {
                int[] coords = Array.ConvertAll(item.Split(','), int.Parse);
                points.Add(new DataPoint(coords[0], coords[1]));
            });

            return points;
        }
    }
}
