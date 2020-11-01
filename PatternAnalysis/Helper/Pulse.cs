using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternAnalysis.Helper
{
    public class Pulse
    {
        public int Count { get; set; }
        public DataPoint Position { get; set; }

        public Pulse(int count, DataPoint pt)
        {
            Count = count;
            Position = pt;
        }
    }
}
