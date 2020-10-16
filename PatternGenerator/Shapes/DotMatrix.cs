using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatternGenerator.Shapes
{
    class DotMatrix : IShape
    {
        public ObservableCollection<DataPoint> Points { get; set; } = new ObservableCollection<DataPoint>();
        public string Description { get; set; } = "";
        public Dictionary<string, ShapeOptions> Options { get; set; } = new Dictionary<string, ShapeOptions>();

        public DotMatrix()
        {
            Options.Add("N", new ShapeOptions(10, "N * N Square"));
            Options.Add("Side Length", new ShapeOptions(5000, "Side Length of Square"));
        }

        public void Generate()
        {
            Points.Clear();
            double n = this.Options["N"].Value;
            double l = this.Options["Side Length"].Value;
            double min = -l / 2;
            for (double i = 0; i <= l; i += l / n)
            {
                for (double j = 0; j <= l; j += l / n)
                {
                    this.Points.Add(new DataPoint(Math.Round(i + min, 0), Math.Round(j + min, 0)));
                }
            }
        }
        public override string ToString()
        {
            return "DotMatrix";
        }
    }
}
