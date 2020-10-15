using Microsoft.Win32;
using OxyPlot;
using PatternGenerator.Helper;
using PatternGenerator.Interfaces;
using PatternGenerator.IO;
using PatternGenerator.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PatternGenerator.ViewModels
{
    public class PatternGeneratorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand UpdatePatternCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }

        public int RepeatValue { get; set; }
        public bool RandomizePattern { get; set; }
        public string Title { get; set; } = "Pattern Generator";

        public IShape Shape { get; set; }
        public IList<IShape> ShapeList { get; private set; }
        public IShape SelectedShape
        {
            get { return this.Shape; }
            set
            {
                this.Shape = value;
                this.Shape.Generate();
                this.OnPropertyChanged(nameof(this.Shape));
            }
        }

        public PatternGeneratorViewModel()
        {

            ShapeList = new List<IShape>();
            ShapeList.Add(new Circle());
            ShapeList.Add(new Spiral());
            ShapeList.Add(new DotMatrix());

            Shape = ShapeList.First();
            UpdatePatternHandler();

            this.RepeatValue = 10;
            this.RandomizePattern = true;

            this.UpdatePatternCommand = new BaseCommand(true, UpdatePatternHandler);
            this.SaveFileCommand = new BaseCommand(true, SaveFileHandler);
        }

        private void UpdatePatternHandler()
        {
            this.Shape.Generate();
            this.OnPropertyChanged(nameof(this.Shape));
        }

        private void SaveFileHandler()
        {
            JsonWrite.ExportPattern(this.Shape.Points.ToList<DataPoint>(), this.Shape.ToString(), RepeatValue, RandomizePattern);
        }

    }
}
