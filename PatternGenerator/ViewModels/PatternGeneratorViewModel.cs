using Infrastructure.Oxyplot;
using OxyPlot;
using PatternGenerator.Enums;
using PatternGenerator.Interfaces;
using PatternGenerator.IO;
using PatternGenerator.Shapes;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace PatternGenerator.ViewModels
{
    public class PatternGeneratorViewModel : BindableBase
    {
        public DelegateCommand UpdatePatternCommand { get; set; }
        public DelegateCommand SaveFileCommand { get; set; }

        public int RepeatValue { get; set; }
        private SpotDistributionTypes _spotDistributionTypes;
        public SpotDistributionTypes SpotDistributionTypes
        {
            get { return _spotDistributionTypes; }
            set { SetProperty(ref _spotDistributionTypes, value); }
        }
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
            get { return Shape; }
            set
            {
                Shape = value;
                Shape.Generate();
                UpdatePatternHandler();
            }
        }

        public PatternGeneratorViewModel()
        {

            PlotModel = PlotModelHelper.CreateScatterPlotInvX();

            ShapeList = new List<IShape>
            {
                new Circle(),
                new Spiral(),
                new DotMatrix(),
                new Cross(),
                new Star()
            };

            Shape = ShapeList.First();
            UpdatePatternHandler();

            RepeatValue = 10;
            SpotDistributionTypes = SpotDistributionTypes.Random;

            UpdatePatternCommand = new DelegateCommand(UpdatePatternHandler);
            SaveFileCommand = new DelegateCommand(SaveFileHandler);
        }

        private void UpdatePatternHandler()
        {
            Shape.Generate();
            
            PlotModel.Series.Clear();
            PlotModel.Series.Add(PlotModelHelper.CreateScatterSerie(Shape.Points.ToList()));
            PlotModel.InvalidatePlot(true);
        }

        private void SaveFileHandler()
        {
            JsonWrite.ExportPattern(Shape.Points.ToList(), Shape.ToString(), RepeatValue, SpotDistributionTypes);
        }

    }
}
