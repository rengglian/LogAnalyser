using LogAnalyser.Interfaces;
using OxyPlot;
using System;
using System.Collections.Generic;

namespace LogAnalyser.Helper
{
    class Histogram : IHistogram
    {
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public bool Create(List<double> source, int binSize)
        {
            SortedDictionary<int, int> bins = new SortedDictionary<int, int>();
            foreach (var value in source)
            {
                var binIndex = binSize*((((int)(Math.Floor(value))) / binSize ) + 1 );
                if (!bins.ContainsKey(binIndex))
                {
                    bins.Add(binIndex, 0);
                }
                bins[binIndex]++;
            }

            this.Points.Clear();
            foreach (var item in bins)
            {
                this.Points.Add(new DataPoint(item.Key, item.Value));
            }

            return true;
        }
    }
}
