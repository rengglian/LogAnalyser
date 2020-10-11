using PatternAnalysis.Extension;
using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using PatternAnalysis.IO;
using Microsoft.Win32;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace PatternAnalysis.ViewModels
{
    public class PatternAnalysisViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand DataSet1FileCommand { get; set; }
        public ICommand DataSet2FileCommand { get; set; }
        public ICommand DataSet3FileCommand { get; set; }
        public ICommand Analyse12Command { get; set; }
        public ICommand Analyse23Command { get; set; }
        public IHistogram Histogram { get; set; } = new Histogram();
        public IPattern Pattern1 { get; set; }
        public IPattern Pattern2 { get; set; }
        public IPattern Pattern3 { get; set; }
        public ObservableCollection<DataPoint> HistoSet { get; private set; } = new ObservableCollection<DataPoint>();
        public int BinValue { get; set; }
        public string Title { get; set; }

        public PatternAnalysisViewModel()
        {
            this.DataSet1FileCommand = new BaseCommand(true, DataSet1FileHandler);
            this.DataSet2FileCommand = new BaseCommand(true, DataSet2FileHandler);
            this.DataSet3FileCommand = new BaseCommand(true, DataSet3FileHandler);
            this.Analyse12Command = new BaseCommand(true, Analyse12Handler);
            this.Analyse23Command = new BaseCommand(true, Analyse23Handler);
            BinValue = 20;
            Title = "Test";

        }

        private void Analyse12Handler()
        {
            List<double> deltaList = new List<double>();
            for (int i = 0; i < this.Pattern2.Points.Count; i++)
            {
                var p1 = new Point { X = this.Pattern2.Points[i].X, Y = this.Pattern2.Points[i].Y };
                var p2 = new Point { X = this.Pattern3.Points[i].X, Y = this.Pattern3.Points[i].Y };
                deltaList.Add((p1 - p2).Length);
            }
            var histo = Histogram.Create(deltaList, BinValue);
            this.HistoSet = Histogram.Points.ToObservableCollection();
            this.OnPropertyChanged(nameof(this.HistoSet));
        }

        private void Analyse23Handler()
        {
            List<double> deltaList = new List<double>();
            for (int i = 0; i < this.Pattern2.Points.Count; i++)
            {
                var p1 = new Point { X = this.Pattern2.Points[i].X, Y = this.Pattern2.Points[i].Y };
                var p2 = new Point { X = this.Pattern3.Points[i].X, Y = this.Pattern3.Points[i].Y };
                deltaList.Add((p1 - p2).Length);
            }
            var histo = Histogram.Create(deltaList, BinValue);
            this.HistoSet = Histogram.Points.ToObservableCollection();
            this.OnPropertyChanged(nameof(this.HistoSet));
        }

        private void DataSet1FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
  
                this.Pattern1 = new Pattern(openFileDialog.FileName);
                this.OnPropertyChanged(nameof(this.Pattern1));


                /*{
                    this.FileName = string.Empty;
                    MessageBox.Show("Invalid file!");
                }*/
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
