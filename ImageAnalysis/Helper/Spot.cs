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
        public Point DrawPosition { get; set; }
        public float Diameter { get; set; }
        public SolidColorBrush Color { get; set; }

        public Spot(Point pt, float d, SolidColorBrush c)
        {
            this.Position = pt;
            this.DrawPosition = new Point(pt.X - d/2, pt.Y -d/2);
            this.Diameter = d;
            this.Color = c;
        }
        public double Distance(Point pt)
        {
            double dx = this.Position.X - pt.X;
            double dy = this.Position.Y - pt.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
