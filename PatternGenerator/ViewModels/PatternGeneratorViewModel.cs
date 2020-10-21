using Infrastructure.Oxyplot;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using PatternGenerator.IO;
using PatternGenerator.Shapes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PatternGenerator.ViewModels
{
    public class PatternGeneratorViewModel : BindableBase
    {
        public DelegateCommand UpdatePatternCommand { get; set; }
        public DelegateCommand SaveFileCommand { get; set; }

        public int RepeatValue { get; set; }
        public bool RandomizePattern { get; set; }
        public string Title { get; set; } = "Pattern Generator";

        private IShape shape;
        public IShape Shape
        {
            get { return shape; }
            set { SetProperty(ref shape, value); }
        }

        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get { return plotModel; }
            set { SetProperty(ref plotModel, value); }
        }

        public IList<IShape> ShapeList { get; private set; }
        public IShape SelectedShape
        {
            get { return this.Shape; }
            set
            {
                this.Shape = value;
                this.Shape.Generate();
                UpdatePatternHandler();
            }
        }

        public PatternGeneratorViewModel()
        {

            PlotModel = PlotModelHelper.CreateScatterPlot();

            ShapeList = new List<IShape>
            {
                new Circle(),
                new Spiral(),
                new DotMatrix()
            };

            this.Shape = ShapeList.First();
            UpdatePatternHandler();

            this.RepeatValue = 10;
            this.RandomizePattern = true;

            this.UpdatePatternCommand = new DelegateCommand(UpdatePatternHandler);
            this.SaveFileCommand = new DelegateCommand(SaveFileHandler);
        }

        private void UpdatePatternHandler()
        {
            this.Shape.Generate();
            
            PlotModel.Series.Clear();
            PlotModel.Series.Add(PlotModelHelper.CreateScatterSerie(this.Shape.Points.ToList<DataPoint>()));
            PlotModel.InvalidatePlot(true);
        }

        private void SaveFileHandler()
        {
            JsonWrite.ExportPattern(this.Shape.Points.ToList<DataPoint>(), this.Shape.ToString(), RepeatValue, RandomizePattern);
        }

    }
}
