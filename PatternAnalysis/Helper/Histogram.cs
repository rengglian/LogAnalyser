using OxyPlot;
using PatternAnalysis.Interfaces;
using System;
using System.Collections.Generic;

namespace PatternAnalysis.Helper
{
    public class Histogram
    {
        public static List<DataPoint> Create(IPattern from, IPattern to, int binSize)
        {
            var deltaList = Distance(from.Points, to.Points);
            var Points = CreateBins(deltaList, binSize);
            return Points;
        }

        public static List<double> Distance(List<DataPoint> from, List<DataPoint> to)
        {
            List<double> distance = new List<double>();

            if (from.Count != to.Count) return distance;

            for (int i = 0; i < from.Count; i++)
            {
                var p1 = new System.Windows.Point { X = from[i].X, Y = from[i].Y };
                var p2 = new System.Windows.Point { X = to[i].X, Y = to[i].Y };
                distance.Add((p1 - p2).Length);
            }
            return distance;
        }

        public static List<DataPoint> CreateBins(List<double> samples, int binSize)
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

            List<DataPoint> Points = new List<DataPoint>();
            foreach (var item in bins)
            {
                Points.Add(new DataPoint(item.Key, item.Value));
            }

            return Points;
        }
    }
}
