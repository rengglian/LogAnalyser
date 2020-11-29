using OxyPlot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace PatternGenerator.IO
{
    public class JsonWrite
    {

        public static void ExportPattern(List<Point> pattern, string description, int repeatFactor, bool randomized)
        {
            description += "_" + pattern.Count + "x" + repeatFactor;
            description += randomized ? "_randomized" : "";
            var fileName = DateTime.Now.ToString("HHmmss_") + description + ".json";
            var filePath = Path.Combine("./", fileName);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString;
            var rng = new Random();

            pattern = pattern.SelectMany(x => Enumerable.Repeat(x, repeatFactor)).ToList();
            pattern = randomized ? pattern.OrderBy(a => rng.Next()).ToList(): pattern;

            List<string> strPattern = new List<string>();
            pattern.ForEach(pt =>
           {
               strPattern.Add(new string($"{pt.X},{pt.Y}"));
           });

            jsonString = JsonSerializer.Serialize(strPattern, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
