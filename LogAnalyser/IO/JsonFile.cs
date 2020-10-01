using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Text.Json;
using OxyPlot;
using LogAnalyser.Interfaces;

namespace PatternAnalyser
{
    public class JsonFile : IJsonFile
    {
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public bool Read(string fileName)
        {
            var jsonString = File.ReadAllText(fileName);
            var strList = JsonSerializer.Deserialize<List<string>>(jsonString);

            this.Points.Clear();

            foreach(var item in strList)
            {
                int[] coords = Array.ConvertAll(item.Split(','), int.Parse);
                this.Points.Add(new DataPoint(coords[0], coords[1]));
            }
            return true;
        }
    }
}
