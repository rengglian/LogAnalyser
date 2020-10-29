using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Oxyplot
{
    public class PlotModelHelper
    {
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

            var customAxis = new RangeColorAxis() { 
                Key = "customColors", 
                Position = AxisPosition.None 
            };
            customAxis.AddRange(0, 100, OxyColors.LightGreen);
            customAxis.AddRange(100, 200, OxyColors.Green);
            customAxis.AddRange(200, 300, OxyColors.Orange);
            customAxis.AddRange(300, 500, OxyColors.DarkOrange);
            customAxis.AddRange(500, 10000, OxyColors.DarkRed);
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

        public static PlotModel CreateHistogramm()
        {
            var plotModel = new PlotModel();

            return plotModel;
        }

        public static ScatterSeries CreateScatterSerie(List<DataPoint> pts)
        {
            var series = new ScatterSeries()
            {
                ColorAxisKey = "ColorAxis",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            var maxDist = pts.Max(pt => (Math.Pow(pt.X, 2) + Math.Pow(pt.Y, 2)));

            foreach (DataPoint pt in pts)
            {
                series.Points.Add(new ScatterPoint(pt.X, pt.Y) { Value = (Math.Pow(pt.X, 2) + Math.Pow(pt.Y, 2)) / maxDist });
            }
            return series;
        }

        public static ScatterSeries CreateScatterSerie(List<DataPoint> pts, List<DataPoint> rev)
        {
            var series = new ScatterSeries()
            {
                ColorAxisKey = "customColors",
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };

            List<double> deltaList = new List<double>();
            if (pts.Count == rev.Count)
            {
                for(int i = 0; i < pts.Count; i++)
                {
                    var p1 = new System.Windows.Point { X = pts[i].X, Y = pts[i].Y };
                    var p2 = new System.Windows.Point { X = rev[i].X, Y = rev[i].Y };
                    deltaList.Add((p1 - p2).Length);
                    series.Points.Add(new ScatterPoint(pts[i].X, pts[i].Y) { Value = deltaList[0] });
                }

            }

            return series;
        }

        public static ColumnSeries CreateColumnSeries(List<DataPoint> pts)
        {
            var series = new ColumnSeries()
            {
                FillColor = OxyColors.Aqua,
                ValueField = "Y",
                ItemsSource = pts
            };
            return series;
        }
    }
}
