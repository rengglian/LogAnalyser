using OxyPlot;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace PatternGenerator.Shapes
{
    public class Circle : IShape
    {
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public string Description { get; set; } = "";
        public Dictionary<string, int> Options { get; set; } = new Dictionary<string, int>();

        public Circle()
        {
            Options.Add("R1", 3000);
            Options.Add("R2", -1);
            Options.Add("Steps", 20);
        }

        public void Generate()
        {

            double min = 0;
            double max = (2 * Math.PI) - (2 * Math.PI) / this.Options["Steps"];
            List<double> stepList = Enumerable.Range(0, this.Options["Steps"])
                 .Select(i => min + (max - min) * ((double)i / (this.Options["Steps"] - 1))).ToList();

            this.Options["R2"] = this.Options["R2"] == -1 ? this.Options["R1"] : this.Options["R2"];

            stepList.ForEach(stp =>
            {
                this.Points.Add(new DataPoint(Math.Round(this.Options["R1"] * Math.Sin(stp), 0), Math.Round(this.Options["R2"] * Math.Cos(stp), 0)));
            });
        }
        public override string ToString()
        {
            return "Circle";
        }
    }
}
