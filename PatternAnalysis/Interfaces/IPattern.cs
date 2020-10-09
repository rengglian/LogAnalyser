using OxyPlot;
using System.Collections.Generic;
using System.Windows;

namespace PatternAnalysis.Helper
{
    public interface IPattern
    {
        Point Center { get; set; }
        string CheckSum { get; set; }
        List<DataPoint> Points { get; set; }
    }
}