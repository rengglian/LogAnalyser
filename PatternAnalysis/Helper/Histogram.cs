using PatternAnalysis.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PatternAnalysis.Helper
{
    public class Histogram
    {
        public static List<Point> Create(IPattern from, IPattern to, int binSize)
        {
            var deltaList = Distance(from.Points, to.Points);
            var Points = CreateBins(deltaList, binSize);
            return Points;
        }

        public static List<double> Distance(List<Point> from, List<Point> to)
        {
            List<double> distance = new List<double>();

            if (from.Count != to.Count) return distance;

            for (int i = 0; i < from.Count; i++)
            {
                distance.Add((from[i] - to[i]).Length);
            }
            return distance;
        }

        public static List<Point> CreateBins(List<double> samples, int binSize)
        {
            SortedDictionary<int, int> bins = new SortedDictionary<int, int>();
            foreach (var value in samples)
            {
                var binIndex = binSize * ((((int)(Math.Floor(value))) / binSize) + 1);
                if (!bins.ContainsKey(binIndex))
                {
                    bins.Add(binIndex, 0);
                }
                bins[binIndex]++;
            }

            List<Point> Points = new List<Point>();
            foreach (var item in bins)
            {
                Points.Add(new Point(item.Key, item.Value));
            }

            return Points;
        }
    }
}
