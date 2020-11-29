﻿using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PatternGenerator.Shapes
{
    class Spiral : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, ShapeOptions> Options { get; set; } = new Dictionary<string, ShapeOptions>();

        public Spiral()
        {
            Options.Add("Radius", new ShapeOptions(5000, "Outer most radius of spiral"));
            Options.Add("Revolutions", new ShapeOptions(5, "Number of revolutions"));
            Options.Add("Steps Size", new ShapeOptions(400, "Distance between points"));
        }

        public void Generate()
        {
            Points.Clear();

            double step_size = Options["Steps Size"].Value;
            double revolutions = Options["Revolutions"].Value;
            double radius = Options["Radius"].Value;
            double rotation = Math.PI / 2;
            double maxTheta = revolutions * 2 * Math.PI;
            double delta_r = radius / maxTheta;
            double theta = step_size / delta_r;

            Points.Add(new Point(0.0, 0.0));

            while (theta <= maxTheta)
            {

                double next_dist = delta_r * theta;
                double new_theta = theta + rotation;
 
                Points.Add(new Point(Math.Round(Math.Cos(new_theta) * next_dist, 0), Math.Round(Math.Sin(new_theta) * next_dist, 0)));
                theta += step_size / next_dist;
            }
        }
        public override string ToString()
        {
            return "Spiral";
        }
    }
}
