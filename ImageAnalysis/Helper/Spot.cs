using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Text;
using System.Windows;

namespace ImageAnalysis.Helper
{
    public class Spot
    {
        public Point Position { get; set; }
        public int Radius { get; set; }

        public Spot(Point pt, int r)
        {
            Position = pt;
            Radius = r;
        }
    }
}
