using OxyPlot;
using PatternGenerator.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatternGenerator.Interfaces
{
    public interface IShape
    {
        string Description { get; set; }
        ObservableCollection<DataPoint> Points { get; set; }
        public Dictionary<string, ShapeOptions> Options { get; set; }
        public void Generate();
    }
}