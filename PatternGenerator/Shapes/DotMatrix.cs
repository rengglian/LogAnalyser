﻿using OxyPlot;
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
            Options.Add("Rotation", new ShapeOptions(0, "Rotation in degree"));
        }

        public void Generate()
        {
            Points.Clear();
            double n = Options["N"].Value;
            double l = Options["Side Length"].Value;
            double min = -l / 2;
            var s = Math.Sin(Options["Rotation"].Value * Math.PI / 180);
            var c = Math.Cos(Options["Rotation"].Value * Math.PI / 180);
            for (double i = min; i < l / 2+1; i += l / n)
            {
                for (double j = min; j < l / 2+1; j += l / n)
                {
                    var x = i;
                    var y = j;

                    var x_new = c * x - s * y;
                    var y_new = s * x + c * y;
                    Points.Add(new DataPoint(Math.Round(x_new, 0), Math.Round(y_new, 0)));
                }
            }
        }
        public override string ToString()
        {
            return "DotMatrix";
        }
    }
}
