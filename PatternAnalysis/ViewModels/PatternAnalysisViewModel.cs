﻿using PatternAnalysis.Extension;
using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using PatternAnalysis.IO;
using Microsoft.Win32;
using OxyPlot;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

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
        public Dictionary<string, double> CalibMatrix { get; set; } = new Dictionary<string, double>();

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
            Point2f[] from = new Point2f[this.Pattern3.Points.Count];
            Point2f[] to = new Point2f[this.Pattern2.Points.Count];
            
            int i = 0;
            this.Pattern3.Points.ForEach(pt =>
            {
                from[i++] = new Point2f((float)pt.X, (float)pt.Y);
            });

            i = 0;

            this.Pattern2.Points.ForEach(pt =>
            {
                to[i++] = new Point2f((float)pt.X, (float)pt.Y);
            });

            Mat affineMatrix = Cv2.EstimateAffine2D(InputArray.Create(from), InputArray.Create(to));

            this.CalibMatrix = AffineMatrix.TransformToDict(affineMatrix, this.Pattern3.Center);
            this.OnPropertyChanged(nameof(this.CalibMatrix));
        }

        private void Analyse23Handler()
        {
            List<double> deltaList = new List<double>();
            for (int i = 0; i < this.Pattern2.Points.Count; i++)
            {
                var p1 = new System.Windows.Point { X = this.Pattern2.Points[i].X, Y = this.Pattern2.Points[i].Y };
                var p2 = new System.Windows.Point { X = this.Pattern3.Points[i].X, Y = this.Pattern3.Points[i].Y };
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
