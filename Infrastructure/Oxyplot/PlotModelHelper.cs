using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Infrastructure.Oxyplot
{
    public class PlotModelHelper
    {
        public static PlotModel CreateScatterPlotInvX()
        {
            var plotModel = new PlotModel();


            var color_axis = new LinearColorAxis
            {
                Key = "ColorAxis",
                Maximum = 1,
                Minimum = 0,
                Position = AxisPosition.None
            };
            plotModel.Axes.Add(color_axis);

            var customAxis = new RangeColorAxis() {
                Key = "customColors",
                Position = AxisPosition.None
            };
            customAxis.AddRange(0, 50, OxyColors.LightGreen);
            customAxis.AddRange(50, 100, OxyColors.Green);
            customAxis.AddRange(100, 200, OxyColors.DarkGreen);
            customAxis.AddRange(200, 300, OxyColors.Orange);
            customAxis.AddRange(300, 500, OxyColors.DarkOrange);
            customAxis.AddRange(500, 100000, OxyColors.DarkRed);
            plotModel.Axes.Add(customAxis);


            var x_axis = new LinearAxis
            {

                Position = OxyPlot.Axes.AxisPosition.Bottom,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 1,
                EndPosition = 0
            };

            plotModel.Axes.Add(x_axis);
            var y_axis = new LinearAxis
            {
                Position = AxisPosition.Left,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 1,
                EndPosition = 0
            };
            plotModel.Axes.Add(y_axis);
            return plotModel;
        }

        public static PlotModel CreateScatterPlot()
        {
            var plotModel = new PlotModel();


            var color_axis = new LinearColorAxis
            {
                Key = "ColorAxis",
                Maximum = 1,
                Minimum = 0,
                Position = AxisPosition.None
            };
            plotModel.Axes.Add(color_axis);

            var x_axis = new LinearAxis
            {

                Position = OxyPlot.Axes.AxisPosition.Bottom,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 0,
                EndPosition = 1
            };

            plotModel.Axes.Add(x_axis);
            var y_axis = new LinearAxis
            {
                Position = AxisPosition.Left,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 0,
                EndPosition = 1
            };
            plotModel.Axes.Add(y_axis);
            return plotModel;
        }

        public static PlotModel CreateHistogramm()
        {
            var plotModel = new PlotModel();

            return plotModel;
        }

        public static ScatterSeries CreateScatterSerie(List<Point> pts)
        {
            var series = new ScatterSeries()
            {
                ColorAxisKey = "ColorAxis",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            var val = 1.0 / pts.Count;
            foreach (Point pt in pts)
            {
                val += 1.0 / pts.Count;
                series.Points.Add(new ScatterPoint(pt.X, pt.Y) { Value = val });
            }
            return series;
        }

        public static ScatterSeries CreateMirrorSerie(List<Point> pts)
        {
            var series = new ScatterSeries()
            {
                ColorAxisKey = "ColorAxis",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            var val = 1.0 / pts.Count;
            foreach (Point pt in pts)
            {
                val += 1.0 / pts.Count;
                series.Points.Add(new ScatterPoint(val, pt.X) { Value = val });
                series.Points.Add(new ScatterPoint(val, -pt.Y) { Value = val });
            }
            return series;
        }

        public static ScatterSeries CreateHotZone(Point center, int radius, OxyColor color)
        {
            var series = new ScatterSeries()
            {
                MarkerStroke = color,
                MarkerType = MarkerType.Circle,
                MarkerSize = 1,
                MarkerStrokeThickness = 1,
                MarkerFill = OxyColors.Transparent
            };

            double min = 0;
            double max = (2 * Math.PI) - (2 * Math.PI) / 50;
            List<double> stepList = Enumerable.Range(0, 50)
                 .Select(i => min + (max - min) * ((double)i / (50 - 1))).ToList();


            List<Point> Points = new List<Point>();
            stepList.ForEach(stp =>
            {
                Points.Add(new Point(Math.Round(radius * Math.Sin(stp), 0), Math.Round(radius * Math.Cos(stp), 0)));
            });


            foreach (Point pt in Points)
            {
                series.Points.Add(new ScatterPoint(pt.X+center.X, pt.Y + center.Y) { Value = 1.0 });
            }

            return series;
        }

        public static ScatterSeries CreateScatterSerie(List<Point> pts, OxyColor color)
        {
            var series = new ScatterSeries()
            {
                MarkerFill = color,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            var test = pts.GroupBy(p => p);
            double max = test.Max(c => c.Count());

            foreach (var grp in test)
            {
                series.Points.Add(new ScatterPoint(grp.Key.X, grp.Key.Y) { Value = (double)(grp.Count()) / max });
            }

            return series;
        }

        public static ScatterSeries CreateScatterSerie(List<Point> pts, List<double> distance)
        {
            var series = new ScatterSeries()
            {
                ColorAxisKey = "customColors",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            if (pts.Count == distance.Count)
            {
                for(int i = 0; i < pts.Count; i++)
                {
                    series.Points.Add(new ScatterPoint(pts[i].X, pts[i].Y) { Value = distance[i] });
                }

            }

            return series;
        }

        public static BarSeries CreateBarSeries(List<DataPoint> pts)
        {

            var series = new BarSeries()
            {
                IsStacked = false,
                XAxisKey = "x",
                YAxisKey = "y",
                FillColor = OxyColors.Aqua,
                ValueField = "Y",
                ItemsSource = pts,
                LabelPlacement = LabelPlacement.Base,
                LabelFormatString = "{0}"
            };
            return series;
        }
    }
}
