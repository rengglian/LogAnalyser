﻿using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using Microsoft.Win32;
using OxyPlot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;
using Prism.Mvvm;
using Infrastructure.Oxyplot;
using OxyPlot.Axes;
using System.Linq;
using Prism.Services.Dialogs;

namespace PatternAnalysis.ViewModels
{
    public class PatternAnalysisViewModel : BindableBase
    {

        private readonly IDialogService _dialogService;

        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand OpenDataSetCommand { get; set; }
        public DelegateCommand TransformMatrixCommand { get; set; }
        public DelegateCommand CreateHistogramCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }
        public DelegateCommand CompareCommand { get; set; }

        private ObservableCollection<IPattern> patternList;
        public ObservableCollection<IPattern> PatternList
        {
            get { return patternList; }
            set { SetProperty(ref patternList, value); }
        }
        private IPattern _selectedA { get; set; }
        public IPattern SelectedA
        {
            get { return this._selectedA; }
            set { this._selectedA = value; }
        }

        private IPattern _selectedB { get; set; }
        public IPattern SelectedB
        {
            get { return this._selectedB; }
            set { this._selectedB = value; }
        }

        public int BinValue { get; set; }
        public string Title { get; set; }
        private Dictionary<string, double> calibMatrix;
        public Dictionary<string, double> CalibMatrix
        {
            get { return calibMatrix; }
            set { SetProperty(ref calibMatrix, value); }
        }

        private PlotModel plotModelPattern;
        public PlotModel PlotModelPattern
        {
            get { return plotModelPattern; }
            set { SetProperty(ref plotModelPattern, value); }
        }

        private PlotModel plotModelHisto;
        public PlotModel PlotModelHisto
        {
            get { return plotModelHisto; }
            set { SetProperty(ref plotModelHisto, value); }
        }

        public PatternAnalysisViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {

            _dialogService = dialogService;

            PlotModelPattern = PlotModelHelper.CreateScatterPlot();
            PlotModelHisto = PlotModelHelper.CreateHistogramm();

            patternList = new ObservableCollection<IPattern>();

            OpenDataSetCommand = new DelegateCommand(OpenDataSetHandler);
            TransformMatrixCommand = new DelegateCommand(TransformMatrixHandler);
            CreateHistogramCommand = new DelegateCommand(CreateHistogramHandler);
            SendCommand = new DelegateCommand(SendHandler);
            CompareCommand = new DelegateCommand(CompareHandler);

            BinValue = 20;
            Title = "Test";
            _eventAggregator = eventAggregator;
        }

        private void CompareHandler()
        {
            var patternA = JsonSerializer.Serialize(SelectedA.Points);
            var patternB = JsonSerializer.Serialize(SelectedB.Points);
            _dialogService.ShowPatternCompareDialog(patternA, patternB, r =>{ });
        }

        private void CreateHistogramHandler()
        {
            
            this.patternList.Add(new Pattern("None"));

            var histoSet = Histogram.Create(SelectedA, SelectedB, BinValue);
           
            PlotModelHisto.Series.Clear();
            PlotModelHisto.Series.Add(PlotModelHelper.CreateBarSeries(histoSet));
            PlotModelHisto.Axes.Add(new CategoryAxis { Position = AxisPosition.Bottom, Key = "y", Angle = 90, ItemsSource = histoSet, LabelField = "X"});
            PlotModelHisto.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, Key = "x" });

            PlotModelHisto.InvalidatePlot(true);
        }

        private void OpenDataSetHandler()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.patternList.Add(new Pattern(openFileDialog.FileName));
                                
                var series = PlotModelHelper.CreateScatterSerie(this.patternList.Last().Points, patternList.Last().Color);
                
                PlotModelPattern.Series.Add(series);
                PlotModelPattern.InvalidatePlot(true);
            }
        }

        private void SendHandler()
        {
            var json = JsonSerializer.Serialize(SelectedA.Points);
            _eventAggregator.GetEvent<PatternSendEvent>().Publish(json);
        }

        private void TransformMatrixHandler()
        {

            this.CalibMatrix = AffineMatrix.CalculateMatrix(SelectedA, SelectedB);

            var test = AffineMatrix.Decompose(this.CalibMatrix);


            this.patternList.Add(new Pattern("None"));

            this.patternList.Last().Points = AffineMatrix.CalculateBack(SelectedA.Points, CalibMatrix);

            var series = PlotModelHelper.CreateScatterSerie(this.patternList.Last().Points);
            PlotModelPattern.Series.Add(series);
            PlotModelPattern.InvalidatePlot(true);

            //var histoSet = Histogram.Create(SelectedB, this.patternList.Last(), BinValue);
            /*
            PlotModelHisto.Series.Clear();
            PlotModelHisto.Series.Add(PlotModelHelper.CreateColumnSeries(histoSet));
            PlotModelHisto.Axes.Add(new OxyPlot.Axes.CategoryAxis { Angle = 90, ItemsSource = histoSet, LabelField = "X" });
            PlotModelHisto.InvalidatePlot(true);*/
        }
    }
}
