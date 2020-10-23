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
        public float Diameter { get; set; }
        public SolidColorBrush Color { get; set; }

        public Spot(Point pt, float d, SolidColorBrush c)
        {
            this.Position = pt;
            this.Diameter = d;
            this.Color = c;
        }
    }
}
