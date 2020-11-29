using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PatternGenerator.Shapes
{
    public class Circle : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, ShapeOptions> Options { get; set; } = new Dictionary<string, ShapeOptions>();

        public Circle()
        {
            Options.Add("R1", new ShapeOptions(3000, "Major radius"));
            Options.Add("R2", new ShapeOptions(-1, "Minor radius [-1 R2==R1]"));
            Options.Add("Steps", new ShapeOptions(20, "Number of datapoints"));
        }

        public void Generate()
        {
            Points.Clear();
            double min = 0;
            double max = (2 * Math.PI) - (2 * Math.PI) / Options["Steps"].Value;
            List<double> stepList = Enumerable.Range(0, Options["Steps"].Value)
                 .Select(i => min + (max - min) * ((double)i / (Options["Steps"].Value - 1))).ToList();

            var r2 = Options["R2"].Value == -1 ? Options["R1"].Value : Options["R2"].Value;

            stepList.ForEach(stp =>
            {
                Points.Add(new Point(Math.Round(Options["R1"].Value * Math.Sin(stp), 0), Math.Round(r2 * Math.Cos(stp), 0)));
            });
        }
        public override string ToString()
        {
            return "Circle";
        }
    }
}
