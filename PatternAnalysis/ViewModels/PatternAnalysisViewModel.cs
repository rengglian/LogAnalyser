using PatternAnalysis.Extension;
using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using Microsoft.Win32;
using OxyPlot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Commands;

namespace PatternAnalysis.ViewModels
{
    public class PatternAnalysisViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DelegateCommand DataSet1FileCommand { get; set; }
        public DelegateCommand DataSet2FileCommand { get; set; }
        public DelegateCommand DataSet3FileCommand { get; set; }
        public DelegateCommand Analyse12Command { get; set; }
        public DelegateCommand Analyse23Command { get; set; }
        public IPattern Pattern1 { get; set; }
        public IPattern Pattern2 { get; set; }
        public IPattern Pattern3 { get; set; }
        public IPattern Pattern4 { get; set; }
        public ObservableCollection<DataPoint> HistoSet { get; private set; } = new ObservableCollection<DataPoint>();
        public int BinValue { get; set; }
        public string Title { get; set; }
        public Dictionary<string, double> CalibMatrix { get; set; } = new Dictionary<string, double>();

        public PatternAnalysisViewModel()
        {
            this.DataSet1FileCommand = new DelegateCommand(DataSet1FileHandler);
            this.DataSet2FileCommand = new DelegateCommand(DataSet2FileHandler);
            this.DataSet3FileCommand = new DelegateCommand(DataSet3FileHandler);
            this.Analyse12Command = new DelegateCommand(Analyse12Handler);
            this.Analyse23Command = new DelegateCommand(Analyse23Handler);
            this.BinValue = 20;
            this.Title = "Test";
        }

        private void Analyse12Handler()
        {

            this.CalibMatrix = AffineMatrix.CalculateMatrix(this.Pattern3, this.Pattern2, true);
            this.OnPropertyChanged(nameof(this.CalibMatrix));

            this.Pattern4 = new Pattern("none");
            this.Pattern4.Points.Clear();

            this.Pattern3.Points.ForEach(pt =>
           {
               var x = this.CalibMatrix["m11"] * pt.X + this.CalibMatrix["m12"] * pt.Y + this.CalibMatrix["m13"];
               var y = this.CalibMatrix["m21"] * pt.X + this.CalibMatrix["m22"] * pt.Y + this.CalibMatrix["m23"];
               
               this.Pattern4.Points.Add(new DataPoint(x, y));
           });
           this.OnPropertyChanged(nameof(this.Pattern4));
           this.HistoSet = Histogram.Create(this.Pattern2, this.Pattern4, BinValue).ToObservableCollection();
           this.OnPropertyChanged(nameof(this.HistoSet));
        }

        private void Analyse23Handler()
        {
            this.HistoSet = Histogram.Create(this.Pattern2, this.Pattern3, BinValue).ToObservableCollection();
            this.OnPropertyChanged(nameof(this.HistoSet));
        }

        private void DataSet1FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {

                this.Pattern1 = new Pattern(openFileDialog.FileName);
                this.OnPropertyChanged(nameof(this.Pattern1));
            }
        }

        private void DataSet2FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.Pattern2 = new Pattern(openFileDialog.FileName);
                this.OnPropertyChanged(nameof(this.Pattern2));
            }
        }

        private void DataSet3FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.Pattern3 = new Pattern(openFileDialog.FileName);
                this.OnPropertyChanged(nameof(this.Pattern3));
            }
        }

    }
}
