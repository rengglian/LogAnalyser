using OxyPlot;
using PatternGenerator.Helper;
using System.Collections.Generic;

namespace PatternGenerator.Interfaces
{
    public interface IShape
    {
        string Description { get; set; }
        List<DataPoint> Points { get; set; }
        public Dictionary<string, ShapeOptions> Options { get; set; }
        public void Generate();
    }
}