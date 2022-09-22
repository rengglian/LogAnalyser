using System;
using System.Windows;
using System.Windows.Media;

namespace ImageAnalysis.Helper;

public class Spot
{
    public Point Position { get; set; }
    public Point DrawPosition { get; set; }
    public float Diameter { get; set; }
    public SolidColorBrush Color { get; set; }
    public int Counter { get; set; } = 0;


    public Spot(Point pt, float d, SolidColorBrush c)
    {
        Position = pt;
        DrawPosition = new Point(pt.X - d/2, pt.Y -d/2);
        Diameter = d;
        Color = c;
    }
    public double Distance(Point pt)
    {
        double dx = Position.X - pt.X;
        double dy = Position.Y - pt.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }
}
