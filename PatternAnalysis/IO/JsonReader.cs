using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Navigation;

namespace PatternAnalysis.IO
{
    public class JsonReader
    { 
        public static List<DataPoint> Read(string fileName)
        {
            List<DataPoint> points = new List<DataPoint>();
            var jsonString = File.Exists(fileName) ? File.ReadAllText(fileName) : "";
            
            List<string> strList = new List<string>();
            try  {
                strList = JsonSerializer.Deserialize<List<string>>(jsonString);
            }
            catch {
                return points;
            }

            strList.ForEach(item =>
            {
                double[] coords = Array.ConvertAll(item.Split(','), double.Parse);
                points.Add(new DataPoint(coords[0], coords[1]));
            });

            return points;
        }
    }
}
