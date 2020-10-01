﻿using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalyser.Interfaces
{
    public interface IHistogram
    {
        List<DataPoint> Points { get; set; }

        public bool Create(List<double> source, int binSize);
    }
}
