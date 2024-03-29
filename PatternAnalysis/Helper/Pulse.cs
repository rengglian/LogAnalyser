﻿using System.Windows;

namespace PatternAnalysis.Helper;

public class Pulse
{
    public int Count { get; set; }
    public Point Position { get; set; }

    public Pulse(int count, Point pt)
    {
        Count = count;
        Position = pt;
    }
}
