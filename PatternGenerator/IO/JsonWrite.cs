using PatternGenerator.Enums;
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

        public static void ExportPattern(List<Point> pattern, string description, int repeatFactor, SpotDistributionTypes spotDistributionTypes)
        {
            var fileName = $"{DateTime.Now.ToString("HHmmss")}_" +
                $"{description}_{ pattern.Count}x{ repeatFactor}_" +
                $"{ spotDistributionTypes}.json";

            var filePath = Path.Combine("./", fileName);
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString;
            var rng = new Random();

            List<Point> extendedPattern = new();

            switch (spotDistributionTypes)
            {
                case SpotDistributionTypes.Static:
                    {
                        extendedPattern = pattern.SelectMany(x => Enumerable.Repeat(x, repeatFactor)).ToList();
                        break;
                    }
                default:
                    {
                        for (int i = 0; i < repeatFactor; i++)
                        {
                            extendedPattern.AddRange(pattern);
                        }
                        break;
                    }
            }

            extendedPattern = spotDistributionTypes == SpotDistributionTypes.Random ? extendedPattern.OrderBy(a => rng.Next()).ToList(): extendedPattern;

            List<string> strPattern = new List<string>();
            extendedPattern.ForEach(pt =>
           {
               strPattern.Add(new string($"{(int)pt.X},{(int)pt.Y}"));
           });

            jsonString = JsonSerializer.Serialize(strPattern, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
