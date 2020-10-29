using PatternAnalysis.Extension;
using PatternAnalysis.Helper;
using PatternAnalysis.Interfaces;
using Microsoft.Win32;
using OxyPlot;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Commands;
using Prism.Events;
using System.Text.Json;
using Infrastructure.Prism.Events;
using Prism.Mvvm;
using Infrastructure.Oxyplot;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot.Axes;

namespace PatternAnalysis.ViewModels
{
    public class PatternAnalysisViewModel : BindableBase, INotifyPropertyChanged
    {

        private readonly IEventAggregator _eventAggregator;
        public DelegateCommand<string> OpenDataSetCommand { get; set; }
        public DelegateCommand TransformMatrixCommand { get; set; }
        public DelegateCommand CreateHistogramCommand { get; set; }
        public DelegateCommand SendCommand { get; set; }

        private Dictionary<string, IPattern> patternList;
        public Dictionary<string, IPattern> PatternList
        {
            get { return patternList; }
            set { SetProperty(ref patternList, value); }
        }

        private string selectedA { get; set; }
        public string SelectedA
        {
            get { return this.selectedA; }
            set { this.selectedA = value; }
        }

        private string selectedB { get; set; }
        public string SelectedB
        {
            get { return this.selectedB; }
            set { this.selectedB = value; }
        }

        public int BinValue { get; set; }
        public string Title { get; set; }
        private Dictionary<string, double> calibMatrix;
        public Dictionary<string, double> CalibMatrix
        {
            get { return calibMatrix; }
            set { SetProperty(ref calibMatrix, value); }
        }

        private Dictionary<string, OxyPlot.Series.ScatterSeries> scatterSeries { get; set; } = new Dictionary<string, OxyPlot.Series.ScatterSeries>();

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

        public PatternAnalysisViewModel(IEventAggregator eventAggregator)
        {
            PlotModelPattern = PlotModelHelper.CreateScatterPlot();
            PlotModelHisto = PlotModelHelper.CreateHistogramm();

            patternList = new Dictionary<string, IPattern>();

            OpenDataSetCommand = new DelegateCommand<string>(OpenDataSetHandler);
            TransformMatrixCommand = new DelegateCommand(TransformMatrixHandler);
            CreateHistogramCommand = new DelegateCommand(CreateHistogramHandler);
            SendCommand = new DelegateCommand(SendHandler);
            BinValue = 20;
            Title = "Test";
            _eventAggregator = eventAggregator;
        }

        private void CreateHistogramHandler()
        {
            var str = "Set4";

            if (!this.patternList.ContainsKey(str)) this.patternList.Add(str, new Pattern("None"));
            else this.patternList[str] = new Pattern("None");

            var histoSet = Histogram.Create(this.patternList[SelectedA], this.patternList[SelectedB], BinValue);
            PlotModelHisto.Series.Clear();
            PlotModelHisto.Series.Add(PlotModelHelper.CreateColumnSeries(histoSet));
            PlotModelHisto.Axes.Add(new OxyPlot.Axes.CategoryAxis { Angle = 90, ItemsSource = histoSet, LabelField = "X" }) ;
            PlotModelHisto.InvalidatePlot(true);
        }

        private void OpenDataSetHandler(string str)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if (!this.patternList.ContainsKey(str)) this.patternList.Add(str, new Pattern(openFileDialog.FileName));
                else this.patternList[str] = new Pattern(openFileDialog.FileName);

                OxyPlot.Series.ScatterSeries series;
                if (str == "Set3" && this.patternList.ContainsKey("Set2")) series = PlotModelHelper.CreateScatterSerie(this.patternList[str].Points, this.patternList["Set2"].Points);
                else series = PlotModelHelper.CreateScatterSerie(this.patternList[str].Points);

                if (!this.scatterSeries.ContainsKey(str)) this.scatterSeries.Add(str, series);
                else this.scatterSeries[str] = series;

                PlotModelPattern.Series.Add(this.scatterSeries[str]);
                PlotModelPattern.InvalidatePlot(true);
            }
        }

        private void SendHandler()
        {
            var json = JsonSerializer.Serialize(this.patternList[SelectedA].Points);
            _eventAggregator.GetEvent<PatternSendEvent>().Publish(json);
        }

        private void TransformMatrixHandler()
        {

            this.CalibMatrix = AffineMatrix.CalculateMatrix(this.patternList[SelectedA], this.patternList[SelectedB]);

            var str = "Set4";
            if (!this.patternList.ContainsKey(str)) this.patternList.Add(str, new Pattern("None"));
            else this.patternList[str] = new Pattern("None");

            this.patternList[SelectedA].Points.ForEach(pt =>
            {
                var x = this.CalibMatrix["m11"] * pt.X + this.CalibMatrix["m12"] * pt.Y + this.CalibMatrix["m13"];
                var y = this.CalibMatrix["m21"] * pt.X + this.CalibMatrix["m22"] * pt.Y + this.CalibMatrix["m23"];

                this.patternList[str].Points.Add(new DataPoint(x, y));
            });

            var series = PlotModelHelper.CreateScatterSerie(this.patternList[str].Points);
            if (!this.scatterSeries.ContainsKey(str)) this.scatterSeries.Add(str, series);
            else this.scatterSeries[str] = series;
            PlotModelPattern.Series.Add(this.scatterSeries[str]);
            PlotModelPattern.InvalidatePlot(true);

            var histoSet = Histogram.Create(this.patternList[SelectedB], this.patternList[str], BinValue);

            PlotModelHisto.Series.Clear();
            PlotModelHisto.Series.Add(PlotModelHelper.CreateColumnSeries(histoSet));
            PlotModelHisto.Axes.Add(new OxyPlot.Axes.CategoryAxis { Angle = 90, ItemsSource = histoSet, LabelField = "X" });
            PlotModelHisto.InvalidatePlot(true);
        }
    }
}
