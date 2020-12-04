using Infrastructure.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternGenerator.Shapes
{
    public class DotMatrix : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, Options> Options { get; set; } = new Dictionary<string, Options>();

        public DotMatrix()
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
            for (double i = min; i < l / 2+1; i += l / n)
            {
                for (double j = min; j < l / 2+1; j += l / n)
                {
                    var x = i;
                    var y = j;

                    var x_new = c * x - s * y;
                    var y_new = s * x + c * y;
                    Points.Add(new Point(Math.Round(x_new, 0), Math.Round(y_new, 0)));
                }
            }
        }
        public override string ToString()
        {
            return "DotMatrix";
        }
    }
}
