using OxyPlot;
using System.Collections.Generic;
using System.Windows;

namespace LogAnalyser.Helper
{
    public interface IPattern
    {
        Point Center { get; set; }
        string CheckSum { get; set; }
        List<DataPoint> Points { get; set; }
    }
}