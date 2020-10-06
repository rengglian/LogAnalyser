using LogAnalyser.Extension;
using LogAnalyser.Helper;
using LogAnalyser.Interfaces;
using Microsoft.Win32;
using OxyPlot;
using PatternAnalyser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace LogAnalyser.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand DataSet1FileCommand { get; set; }
        public ICommand DataSet2FileCommand { get; set; }
        public ICommand DataSet3FileCommand { get; set; }
        public ICommand AnalyseCommand { get; set; }
        public IJsonFile JsonFile { get; set; } = new JsonFile();
        public IHistogram Histogram { get; set; } = new Histogram();
        public int BinValue { get; set; }

        public MainWindowViewModel()
        {
            this.DataSet1FileCommand = new BaseCommand(true, DataSet1FileHandler);
            this.DataSet2FileCommand = new BaseCommand(true, DataSet2FileHandler);
            this.DataSet3FileCommand = new BaseCommand(true, DataSet3FileHandler);
            this.AnalyseCommand = new BaseCommand(true, AnalyseHandler);
            BinValue = 20;

        }

        public void AnalyseHandler()
        {
            List<double> deltaList = new List<double>();
            for (int i = 0; i < this.DataSet1.Count; i++)
            {
                var p1 = new Point { X = this.DataSet1[i].X, Y = this.DataSet1[i].Y };
                var p2 = new Point { X = this.DataSet2[i].X, Y = this.DataSet2[i].Y };
                deltaList.Add((p1 - p2).Length);
            }
            var histo = Histogram.Create(deltaList, BinValue);
            this.HistoSet = Histogram.Points.ToObservableCollection();
            this.OnPropertyChanged(nameof(this.HistoSet));
        }

        public void DataSet1FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (JsonFile.Read(openFileDialog.FileName))
                {
                    this.FileName = openFileDialog.FileName;
                    this.OnPropertyChanged(nameof(this.FileName));
                    this.DataSet1 = JsonFile.Points.ToObservableCollection();
                    this.OnPropertyChanged(nameof(this.DataSet1));
                }
                else
                {
                    this.FileName = string.Empty;
                    MessageBox.Show("Invalid file!");
                }
            }
        }

        public void DataSet2FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (JsonFile.Read(openFileDialog.FileName))
                {
                    this.FileName = openFileDialog.FileName;
                    this.OnPropertyChanged(nameof(this.FileName));
                    this.DataSet2 = JsonFile.Points.ToObservableCollection();
                    this.OnPropertyChanged(nameof(this.DataSet2));
                }
                else
                {
                    this.FileName = string.Empty;
                    MessageBox.Show("Invalid file!");
                }
            }
        }

        public void DataSet3FileHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (JsonFile.Read(openFileDialog.FileName))
                {
                    this.FileName = openFileDialog.FileName;
                    this.OnPropertyChanged(nameof(this.FileName));
                    this.DataSet3 = JsonFile.Points.ToObservableCollection();
                    this.OnPropertyChanged(nameof(this.DataSet3));
                }
                else
                {
                    this.FileName = string.Empty;
                    MessageBox.Show("Invalid file!");
                }
            }
        }

        public string Title { get; private set; }
        public ObservableCollection<DataPoint> DataSet1 { get; private set; } = new ObservableCollection<DataPoint>();
        public ObservableCollection<DataPoint> DataSet2 { get; private set; } = new ObservableCollection<DataPoint>();
        public ObservableCollection<DataPoint> DataSet3 { get; private set; } = new ObservableCollection<DataPoint>();
        public ObservableCollection<DataPoint> HistoSet { get; private set; } = new ObservableCollection<DataPoint>();

        public string FileName { get; set; }
    }
}
