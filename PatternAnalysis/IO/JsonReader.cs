using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace PatternAnalysis.IO;

public class JsonReader
{ 
    public static List<Point> Read(string fileName)
    {
        List<Point> points = new();
        var jsonString = File.Exists(fileName) ? File.ReadAllText(fileName) : "";
        
        List<string> strList = new();
        try  {
            strList = JsonSerializer.Deserialize<List<string>>(jsonString);
        }
        catch {
            return points;
        }

        strList.ForEach(item =>
        {
            double[] coords = Array.ConvertAll(item.Split(','), double.Parse);
            points.Add(new Point(coords[0], coords[1]));
        });

        return points;
    }
}
