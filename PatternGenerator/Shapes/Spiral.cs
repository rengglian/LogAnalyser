using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternGenerator.Shapes
{
    class Spiral : IShape
    {
        public List<DataPoint> Points { get; set; } = new List<DataPoint>();
        public string Description { get; set; } = "";
        public Dictionary<string, ShapeOptions> Options { get; set; } = new Dictionary<string, ShapeOptions>();

        public Spiral()
        {
            Options.Add("R1", new ShapeOptions(3000, "Outer most radius of spiral"));
            Options.Add("Revolutions", new ShapeOptions(3, "Number of revolutions"));
            Options.Add("Steps", new ShapeOptions(200, "Number of datapoints"));
        }

        public void Generate()
        {
            double min = 0;
            double max = (2 * Math.PI * this.Options["Revolutions"].Value);
            List<double> stepList = Enumerable.Range(0, this.Options["Steps"].Value)
                 .Select(i => min + (max - min) * ((double)i / (this.Options["Steps"].Value - 1))).ToList();

            Points.Clear();

            stepList.ForEach(stp =>
            {
                var x = this.Options["R1"].Value / this.Options["Revolutions"].Value * (stp / (2 * Math.PI)) * Math.Cos(stp);
                var y = this.Options["R1"].Value / this.Options["Revolutions"].Value * (stp / (2 * Math.PI)) * Math.Sin(stp);
                this.Points.Add(new DataPoint(Math.Round(x, 0), Math.Round(y, 0)));
            });
        }
        public override string ToString()
        {
            return "Spiral";
        }
    }
}
