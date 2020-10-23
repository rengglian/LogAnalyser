using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ImageAnalysis.Helper
{
    public class Spot
    {
        public Point Position { get; set; }
        public float Radius { get; set; }

        public Spot(Point pt, float r)
        {
            this.Position = pt;
            this.Radius = r;
        }
    }
}
