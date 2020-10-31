using OxyPlot;
using System.Collections.Generic;
using System.Windows;

namespace PatternAnalysis.Interfaces
{
    public interface IPattern
    {
        Point Center { get; set; }
        string CheckSum { get; set; }
        string FileName { get; set; }
        List<DataPoint> Points { get; set; }
        OxyColor Color { get; set; }
        string ToString();
    }
}