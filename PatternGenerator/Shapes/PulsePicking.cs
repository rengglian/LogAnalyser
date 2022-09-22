using Infrastructure.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternGenerator.Shapes
{
    public class PulsePicking : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, Options> Options { get; set; } = new Dictionary<string, Options>();

        public PulsePicking()
        {
            Options.Add("x-th", new Options(10, "every x-th"));
            Options.Add("Distance", new Options(5000, "Distance between two positions"));
            Options.Add("Rotation", new Options(0, "Rotation around center"));
        }

        public void Generate()
        {
            Points.Clear();
            double xth = Options["x-th"].Value;
            double d = Options["Distance"].Value/2.0;

            var s = Math.Sin(Options["Rotation"].Value * Math.PI / 180);
            var c = Math.Cos(Options["Rotation"].Value * Math.PI / 180);
            for (double i = 1; i < xth; i++)
            {
                var x = -d;
                var y = 0.0;

                var new_x = c * x - s * y;
                var new_y = s * x + c * y;

                Points.Add(new Point(Math.Round(new_x,0), Math.Round(new_y, 0)));
            }

            var x2 = d;
            var y2 = 0.0;

            var new_x2 = c * x2 - s * y2;
            var new_y2 = s * x2 + c * y2;
            Points.Add(new Point(Math.Round(new_x2, 0), Math.Round(new_y2, 0)));
        }
        public override string ToString()
        {
            return "Pulse Picking";
        }
    }
}
