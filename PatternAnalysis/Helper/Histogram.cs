using OxyPlot;
using PatternAnalysis.Interfaces;
using System;
using System.Collections.Generic;

namespace PatternAnalysis.Helper
{
    class Histogram
    {
        
        public static List<DataPoint> Create(IPattern from, IPattern to, int binSize)
        {
            List<DataPoint> Points = new List<DataPoint>();
            
            List<double> deltaList = new List<double>();
            for (int i = 0; i < from.Points.Count; i++)
            {
                var p1 = new System.Windows.Point { X = from.Points[i].X, Y = from.Points[i].Y };
                var p2 = new System.Windows.Point { X = to.Points[i].X, Y = to.Points[i].Y };
                deltaList.Add((p1 - p2).Length);
            }


            SortedDictionary<int, int> bins = new SortedDictionary<int, int>();
            foreach (var value in deltaList)
            {
                var binIndex = binSize*((((int)(Math.Floor(value))) / binSize ) + 1 );
                if (!bins.ContainsKey(binIndex))
                {
                    bins.Add(binIndex, 0);
                }
                bins[binIndex]++;
            }

            foreach (var item in bins)
            {
                Points.Add(new DataPoint(item.Key, item.Value));
            }

            return Points;
        }
    }
}
