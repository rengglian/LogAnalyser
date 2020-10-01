using System;
using System.Collections.Generic;
using System.Text;
using OxyPlot;

namespace LogAnalyser.Interfaces
{
    public interface IJsonFile
    {
        List<DataPoint> Points { get; set; }

        public bool Read(string fileName);
    }
}
