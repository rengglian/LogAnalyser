using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatternGenerator.Helper
{
    public class PlotModelHelper
    {
        public static PlotModel Init()
        {
            var plotModel = new PlotModel()
            {

            };
            var x_axis = new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 1,
                EndPosition = 0
            };
            plotModel.Axes.Add(x_axis);
            var y_axis = new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.DimGray,
                StartPosition = 1,
                EndPosition = 0
            };
            plotModel.Axes.Add(y_axis);
            return plotModel;
        }

        public static LineSeries CreateSerie(List<DataPoint> pts)
        {
            var series = new LineSeries
            {
                MarkerFill = OxyColors.Aqua,
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3
            };
            foreach (DataPoint pt in pts)
            {
                series.Points.Add(pt);
            }
            return series;
        }
    }
}
