using PatternGenerator.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternGenerator.Interfaces
{
    public interface IShape
    {
        string Description { get; set; }
        ObservableCollection<Point> Points { get; set; }
        public Dictionary<string, ShapeOptions> Options { get; set; }
        public void Generate();
    }
}