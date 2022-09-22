using Infrastructure.Helper;
using PatternGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace PatternGenerator.Shapes
{
    public class Star : IShape
    {
        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>();
        public string Description { get; set; } = "";
        public Dictionary<string, Options> Options { get; set; } = new Dictionary<string, Options>();

        public Star()
        {
            Options.Add("Spikes", new Options(5, "Spikes of the star"));
            Options.Add("Outer Radius", new Options(3000, "Outer radius"));
            Options.Add("Inner Radius", new Options(1150, "Inner radius"));
            Options.Add("Steps", new Options(5, "Steps / Side"));
        }

        public void Generate()
        {
            Points.Clear();

            List<Point> outerPoints = new();
            List<Point> innerPoints = new();


            double spikes = Options["Spikes"].Value;
            double outerRadius = Options["Outer Radius"].Value;
            double innerRadius = Options["Inner Radius"].Value;
            double steps = Options["Steps"].Value;

            double min = 0;
            double angleSteps = (2 * Math.PI) / Options["Spikes"].Value;
            double max = (2 * Math.PI) - angleSteps;
            
            List<double> outerAngles = Enumerable.Range(0, (int)Options["Spikes"].Value)
                 .Select(i => min + (max - min) * ((double)i / (Options["Spikes"].Value - 1))).ToList();

            outerAngles.ForEach(stp =>
            {
                outerPoints.Add(new Point(Math.Round(outerRadius * Math.Sin(stp), 0), Math.Round(outerRadius * Math.Cos(stp), 0)));
                innerPoints.Add(new Point(Math.Round(innerRadius * Math.Sin(stp + angleSteps/2), 0), Math.Round(innerRadius * Math.Cos(stp + angleSteps / 2), 0)));
            });

            for ( int i = 0; i < outerPoints.Count; i++)
            {
                Points.Add(new Point(outerPoints[i].X, outerPoints[i].Y));
                Points.Add(new Point(innerPoints[i].X, innerPoints[i].Y));

                double leftDeltaX = innerPoints[i].X - outerPoints[i].X;
                double leftDeltaY = innerPoints[i].Y - outerPoints[i].Y;
                double leftSlope = leftDeltaY / leftDeltaX;
                double leftStepSize = leftDeltaX / (steps + 1);

                double rightDeltaX = innerPoints[i - 1 < 0 ? innerPoints.Count - 1 : i - 1].X - outerPoints[i].X;
                double rightDeltaY = innerPoints[i - 1 < 0 ? innerPoints.Count - 1 : i - 1].Y - outerPoints[i].Y;
                double rightSlope = rightDeltaY / rightDeltaX;
                double rightStepSize = rightDeltaX / (steps + 1);

                for ( int j = 1; j <= steps; j++)
                {
                    var leftNewX = outerPoints[i].X + leftStepSize * j;
                    var leftNewY = outerPoints[i].Y + leftSlope * leftStepSize * j;
                    var rightNewX = outerPoints[i].X + rightStepSize * j;
                    var rightNewY = outerPoints[i].Y + rightSlope * rightStepSize * j;

                    Points.Add(new Point(leftNewX, leftNewY));
                    Points.Add(new Point(rightNewX, rightNewY));
                }
            }

        }
        public override string ToString()
        {
            return "Star";
        }
    }
}
