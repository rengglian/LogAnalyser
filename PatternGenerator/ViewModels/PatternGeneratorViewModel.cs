using Microsoft.Win32;
using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using PatternGenerator.IO;
using PatternGenerator.Shapes;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

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

        public IList<IShape> ShapeList { get; private set; }
        public IShape SelectedShape
        {
            get { return this.Shape; }
            set
            {
                this.Shape = value;
                this.Shape.Generate();
            }
        }

        public PatternGeneratorViewModel()
        {

            ShapeList = new List<IShape>();
            ShapeList.Add(new Circle());
            ShapeList.Add(new Spiral());
            ShapeList.Add(new DotMatrix());

            this.Shape = ShapeList.First();
            this.Shape.Generate();

            this.RepeatValue = 10;
            this.RandomizePattern = true;

            this.UpdatePatternCommand = new DelegateCommand(UpdatePatternHandler);
            this.SaveFileCommand = new DelegateCommand(SaveFileHandler);
        }

        private void UpdatePatternHandler()
        {
            this.Shape.Generate();
        }

        private void SaveFileHandler()
        {
            JsonWrite.ExportPattern(this.Shape.Points.ToList<DataPoint>(), this.Shape.ToString(), RepeatValue, RandomizePattern);
        }

    }
}
