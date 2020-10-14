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
        public Dictionary<string, int> Options { get; set; } = new Dictionary<string, int>();

        public Spiral()
        {
            Options.Add("R1", 3000);
            Options.Add("Revolutions", 3);
            Options.Add("Steps", 200);
        }

        public void Generate()
        {
            double min = 0;
            double max = (2 * Math.PI * this.Options["Revolutions"]);
            List<double> stepList = Enumerable.Range(0, this.Options["Steps"])
                 .Select(i => min + (max - min) * ((double)i / (this.Options["Steps"] - 1))).ToList();

            stepList.ForEach(stp =>
            {
                var x = this.Options["R1"] / this.Options["Revolutions"] * (stp / (2 * Math.PI)) * Math.Cos(stp);
                var y = this.Options["R1"] / this.Options["Revolutions"] * (stp / (2 * Math.PI)) * Math.Sin(stp);
                this.Points.Add(new DataPoint(Math.Round(x, 0), Math.Round(y, 0)));
            });
        }
        public override string ToString()
        {
            return "Spiral";
        }
    }
}
