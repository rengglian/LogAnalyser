using Infrastructure.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternGenerator.Shapes
{
    public class Cross : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, Options> Options { get; set; } = new Dictionary<string, Options>();

        public Cross()
        {
            Options.Add("N", new Options(10, "N * N Square"));
            Options.Add("Side Length", new Options(5000, "Side Length of Square"));
            Options.Add("Rotation", new Options(0, "Rotation in degree"));
        }

        public void Generate()
        {
            Points.Clear();
            double n = Options["N"].Value;
            double l = Options["Side Length"].Value;
            double min = -l / 2;
            var s = Math.Sin(Options["Rotation"].Value * Math.PI / 180);
            var c = Math.Cos(Options["Rotation"].Value * Math.PI / 180);
            for (double i = 0; i <= l; i += l / n)
            {
                var x = i + min;
                var y = 0.0;

                var new_x = c * x - s * y;
                var new_y = s * x + c * y;

                Points.Add(new Point(Math.Round(new_x,0), Math.Round(new_y, 0)));

                x = 0.0;
                y = i + min;

                new_x = c * x - s * y;
                new_y = s * x + c * y;
                Points.Add(new Point(Math.Round(new_x, 0), Math.Round(new_y, 0)));
            }
        }
        public override string ToString()
        {
            return "Cross";
        }
    }
}
